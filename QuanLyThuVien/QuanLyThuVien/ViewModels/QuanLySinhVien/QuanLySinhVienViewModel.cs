using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace QuanLyThuVien.ViewModels
{
    public partial class QuanLySinhVienViewModel : ObservableObject, IHeaderActionViewModel
    {
        // Các service
        private readonly IStudentService _studentService;
        private readonly ILoanService _loanService;
        private readonly IFacultyService _facultyService;

        // Các thuộc tính cho IHeaderActionViewModel
        public ICommand HeaderButtonCommand => OpenThemStudentPopupCommand;
        public string HeaderButtonLabel => "Thêm sinh viên";
        public bool IsHeaderButtonVisible => true;

        // Thuộc tính phân trang
        private int PageSize = 15;
        private const int WindowSize = 5;
        [ObservableProperty]
        private int currentPage;
        [ObservableProperty]
        private int totalPages;
        [ObservableProperty] private int firstPage;
        [ObservableProperty] private int secondPage;
        [ObservableProperty] private int thirdPage;
        [ObservableProperty] private int fourthPage;
        [ObservableProperty] private int fifthPage;

        // Property cho tìm kiếm 
        [ObservableProperty]
        private string _searchText = string.Empty;
        private CancellationTokenSource? _searchCts;

        [ObservableProperty]
        private StudentItemViewModel? _selectedStudent;

        // Danh sách sinh viên hiển thị
        public ObservableCollection<StudentItemViewModel> StudentList { get; set; } = new();

        public QuanLySinhVienViewModel(IStudentService service, ILoanService loanService, IFacultyService facultyService)
        {
            _studentService = service;
            _loanService = loanService;
            _facultyService = facultyService;

            WeakReferenceMessenger.Default.Register<StudentUpdatedMessage>(this, (r, message) =>
            {
                var studentItemVM = StudentList.FirstOrDefault(student => student.StudentId == message.UpdatedStudent.StudentId);

                if (studentItemVM != null)
                {
                    studentItemVM.RefreshFromModel(message.UpdatedStudent);
                }
            });

            WeakReferenceMessenger.Default.Register<StudentAddedMessage>(this, (r, message) =>
            {
                var newStudentItemVM = new StudentItemViewModel(message.NewStudent);
                StudentList.Add(newStudentItemVM);
            });

            _ = LoadAsync();
        }

        // Thao tác tìm kiếm 
        [RelayCommand]
        private async Task LoadAsync()
        {
            CurrentPage = 1;
            TotalPages = await _studentService.GetTotalPages(PageSize, SearchText);
            Debug.WriteLine($"Total Pages: {TotalPages}");
            UpdateWindow();
            await LoadCurrentPageAsync();
        }

        async partial void OnSearchTextChanged(string value)
        {
            await SearchAsync();
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            _searchCts?.Cancel();
            _searchCts = new CancellationTokenSource();
            var token = _searchCts.Token;
            try
            {

                await Task.Delay(350, token);   
                await LoadAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private async Task LoadCurrentPageAsync()
        {
            if (CurrentPage < 1 || CurrentPage > TotalPages)
                return;
            var students = await _studentService.GetStudentsPage(CurrentPage - 1, PageSize, SearchText);
            StudentList.Clear();
            foreach (var student in students)
            {
                StudentList.Add(new StudentItemViewModel(student));
            }
        }


        // Các thao tác thêm, xóa sửa, ban/unban 
        [RelayCommand]
        private async Task ToggleStatus(StudentItemViewModel studentVM)
        {
            if (studentVM == null) return;

            // Lấy trạng thái cũ, phòng khi cần rollback
            string originalStatus = studentVM.AccountStatus;
            string newStatus = originalStatus == "1" ? "0" : "1";

            studentVM.Student.AccountStatus = newStatus;
            studentVM.AccountStatus = newStatus;

            try
            {
                await _studentService.UpdateStudentAsync(studentVM.Student);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Lỗi khi cập nhật trạng thái: {ex.Message}", "Lỗi Database", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                // Rollback
                studentVM.AccountStatus = originalStatus;
            }
        }
        [RelayCommand]
        private async Task OpenStudentDetailPopup(StudentItemViewModel? sVM)
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(sVM));
            try
            {
                var stats = await _loanService.GetLoanStatsByStudentIdAsync(sVM.StudentId);
                sVM.TongDaMuon = stats.TotalBorrowed;
                sVM.DangMuon = stats.CurrentlyBorrowed;
                sVM.QuaHan = stats.Overdue;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi load stats: " + ex.Message);
            }
        }

        [RelayCommand]
        private void OpenThemStudentPopup()
        {
            var addStudentVM = new ThemStudentViewModel(_studentService, _facultyService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(addStudentVM));
        }

        [RelayCommand]
        private void OpenSuaStudentPopup(StudentItemViewModel? sVM)
        {
            var suaStudentVM = new SuaStudentViewModel(sVM.Student, _studentService, _facultyService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(suaStudentVM));
        }


        // Thao tác phân trang
        [RelayCommand]
        private void PageSelection(int pageNumber)
        {
            if (pageNumber < 0 || pageNumber > TotalPages)
                return;

            CurrentPage = pageNumber;
            UpdateWindow();
            _ = LoadCurrentPageAsync();
        }

        [RelayCommand]
        private async Task GoToPreviousPage()
        {
            if (CurrentPage <= 1)
                return;

            CurrentPage--;
            UpdateWindow();
            await LoadCurrentPageAsync();
        }

        [RelayCommand]
        private async Task GoToNextPage()
        {
            if (CurrentPage >= TotalPages)
                return;

            CurrentPage++;
            UpdateWindow();
            await LoadCurrentPageAsync();
        }

        private void UpdateWindow()
        {
            if (TotalPages <= 1)
                return;

            int start = CurrentPage - WindowSize / 2;
            if (start < 1)
                start = 1;

            int end = start + WindowSize - 1;
            if (end > TotalPages)
            {
                end = TotalPages;
                start = Math.Max(1, end - WindowSize + 1);
            }

            FirstPage = start;
            SecondPage = start + 1 <= end ? start + 1 : 0;
            ThirdPage = start + 2 <= end ? start + 2 : 0;
            FourthPage = start + 3 <= end ? start + 3 : 0;
            FifthPage = start + 4 <= end ? start + 4 : 0;
        }
    }
}
