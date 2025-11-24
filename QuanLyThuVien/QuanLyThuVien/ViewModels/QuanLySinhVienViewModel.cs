

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace QuanLyThuVien.ViewModels
{
    public partial class QuanLySinhVienViewModel : ObservableObject
    {

        private readonly IStudentService _studentService;
        private readonly ILoanService _loanService;
        private readonly IFacultyService _facultyService;
        public QuanLySinhVienViewModel(IStudentService service, ILoanService loanService, IFacultyService facultyService)
        {
            _studentService = service;
            _loanService = loanService;
            _facultyService = facultyService;
        }

        [ObservableProperty]
        private bool _isDetailPopupVisible = false; 

        [ObservableProperty]
        private StudentItemViewModel? _selectedStudent;

        public ObservableCollection<StudentItemViewModel> Students { get; } = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        private CancellationTokenSource? _searchCts;

        public async Task InitializeAsync()
        {
            await LoadAsync();
        }

        [RelayCommand]
        private async Task LoadAsync()
        {
            var list = await _studentService.GetAllStudentsAsync(_searchText);
            Students.Clear();
            foreach (var s in list.Take(60)) 
            {
                Students.Add(new StudentItemViewModel(s)); 
            }
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


        [RelayCommand]
        private async Task ViewDetail(StudentItemViewModel? sVM)
        {
            if (sVM is null)
                return;
            // Gửi tin nhắn yêu cầu MainViewModel mở Popup cho sinh viên này
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
        private void CloseDetailPopup()
        {
            IsDetailPopupVisible = false;
            SelectedStudent = null; 
        }

        [RelayCommand]
        private void AddNew()
        {
            // TODO: mở popup tạo tài khoản mới (dialog view riêng)
            System.Windows.MessageBox.Show("Mở popup tạo tài khoản sinh viên.");
        }

        [RelayCommand]
        private async Task ToggleStatus(StudentItemViewModel studentVM)
        {
            if (studentVM == null) return;

            // Lấy trạng thái cũ, phòng khi cần rollback
            string originalStatus = studentVM.AccountStatus;
            string newStatus = originalStatus == "Active" ? "Disabled" : "Active"; 

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
        private void EditStudent(StudentItemViewModel? sVM)
        {
            if (sVM is null) 
                return;
            var editVM = new StudentEditViewModel(sVM.Student, _studentService, _facultyService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(editVM));
        }
    }
}
