

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

        private CancellationTokenSource? _searchCts;

        public QuanLySinhVienViewModel(IStudentService service)
        {
            _studentService = service;
        }

        // Gọi từ View.Loaded
        public async Task InitializeAsync()
        {
            await LoadAsync();
        }

        [RelayCommand]
        private async Task LoadAsync()
        {
            var list = await _studentService.GetAllStudentsAsync(searchText);
            Students.Clear();
            foreach (var s in list)
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

        //[RelayCommand]
        //private void ViewDetail(StudentItemViewModel? sVM)
        //{
        //    if (sVM is null)
        //        return;

        //    // Lấy dữ liệu từ các thuộc tính của ItemViewModel
        //    System.Windows.MessageBox.Show(
        //        $"MSSV: {sVM.StudentId}\nHọ tên: {sVM.StudentName}\nEmail: {sVM.Email}\nTrạng thái: {sVM.AccountStatus}",
        //        "Thông tin sinh viên");
        //}

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
                // XỬ LÝ LỖI: Nếu lưu DB thất bại
                System.Windows.MessageBox.Show($"Lỗi khi cập nhật trạng thái: {ex.Message}", "Lỗi Database", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);

                // Rollback: Trả trạng thái trên UI về như cũ
                studentVM.AccountStatus = originalStatus;
            }
            Debug.WriteLine($"Sinh viên {studentVM.StudentName} hiện có trạng thái: {studentVM.AccountStatus} trong VM");
        }
    }
}
