

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

            WeakReferenceMessenger.Default.Register<StudentUpdatedMessage>(this, (r, m) =>
            {
                var itemVM = Students.FirstOrDefault(s => s.StudentId == m.UpdatedStudent.StudentId);

                if (itemVM != null)
                {
                    itemVM.RefreshFromModel(m.UpdatedStudent);
                }
            });

            WeakReferenceMessenger.Default.Register<StudentAddedMessage>(this, (r, m) =>
            {
                var newItemVM = new StudentItemViewModel(m.NewStudent);
                Students.Insert(0, newItemVM);
            });
        }

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
            foreach (var s in list) 
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
        private async Task OpenStudentDetailPopup(StudentItemViewModel? sVM)
        {
            if (sVM is null)
                return;
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
            var addStudentVM = new ThemSinhVienViewModel(_studentService, _facultyService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(addStudentVM));
        }


        [RelayCommand]
        private void OpenSuaStudentPopup(StudentItemViewModel? sVM)
        {
            if (sVM is null) 
                return;
            var suaStudentPopup = new SuaStudentViewModel(sVM.Student, _studentService, _facultyService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(suaStudentPopup));
        }
    }
}
