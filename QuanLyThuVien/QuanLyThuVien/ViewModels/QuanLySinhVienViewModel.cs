

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

        public ObservableCollection<Students> Students { get; } = new();

        [ObservableProperty]
        private string searchText = string.Empty;
        [ObservableProperty]
        private string accountStatus = "1";

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
            foreach (var s in list) Students.Add(s);
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
        private void ViewDetail(Students? s)
        {
            if (s is null) 
                return;
            System.Windows.MessageBox.Show(
                $"MSSV: {s.StudentId}\nHọ tên: {s.StudentName}\nEmail: {s.Email}\nTrạng thái: {s.AccountStatus}",
                "Thông tin sinh viên");
        }

        [RelayCommand]
        private void AddNew()
        {
            // TODO: mở popup tạo tài khoản mới (dialog view riêng)
            System.Windows.MessageBox.Show("Mở popup tạo tài khoản sinh viên.");
        }

        [RelayCommand]
        private void ToggleStatus(Students student)
        {
            if (student == null) return;
            Debug.WriteLine($"Sinh viên truowsc khi doi {student.StudentName} hiện có trạng thái: {student.AccountStatus}");

            _studentService.ChangeStatus(student);

            _ = LoadAsync();

            Debug.WriteLine($"Sinh viên {student.StudentName} hiện có trạng thái: {student.AccountStatus}");
        }
    }
}
