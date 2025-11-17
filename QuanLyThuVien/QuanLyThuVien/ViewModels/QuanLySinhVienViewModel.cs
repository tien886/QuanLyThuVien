

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace QuanLyThuVien.ViewModels
{
    public partial class QuanLySinhVienViewModel : ObservableObject
    {
        private readonly IStudentService _studentService;

        public ObservableCollection<StudentItemViewModel> Students { get;  } = new();

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool _isDetailPopupVisible = false; 

        [ObservableProperty]
        private StudentItemViewModel? _selectedStudent;


        private CancellationTokenSource? _searchCts;

        public QuanLySinhVienViewModel(IStudentService service)
        {
            _studentService = service;
        }

        public async Task InitializeAsync()
        {
            await LoadAsync();
        }

        [RelayCommand]
        private async Task LoadAsync()
        {
            var list = await _studentService.GetAllStudentsAsync(searchText);
            Students.Clear();
            foreach (var s in list.Take(60)) // LẤY ÍT THÔI KẺO LAG
            {
                Students.Add(new StudentItemViewModel(s)); // Chuyển model thành ViewModel item
            }
        }

        // Debounce: gọi từ setter SearchTextChangedCommand
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
            catch (TaskCanceledException) { }
        }

        [RelayCommand]
        private void ViewDetail(StudentItemViewModel? sVM)
        {
            if (sVM is null)
                return;
            SelectedStudent = sVM;
            IsDetailPopupVisible = true;
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
    }
}
