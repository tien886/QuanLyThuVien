using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace QuanLyThuVien.ViewModels
{
    public partial class StudentEditViewModel : ObservableObject
    {
        private readonly IStudentService _studentService;
        private readonly IFacultyService _facultyService;
        private readonly Students _originalStudent; // Giữ tham chiếu gốc để update sau khi Save
        public ObservableCollection<Faculties> FacultyList { get; } = new();
        [ObservableProperty] private string _studentName;
        [ObservableProperty] private string _email;
        [ObservableProperty] private string _phoneNumber;
        [ObservableProperty] private string _faculty;
        [ObservableProperty] private int _selectedFacultyId;
        public string StudentId => _originalStudent.StudentId.ToString();

        public StudentEditViewModel(Students student,
            IStudentService studentService,
            IFacultyService facultyService)
        {
            _studentService = studentService;
            _facultyService = facultyService;
            _originalStudent = student;

            // Copy dữ liệu từ Model gốc sang các biến tạm Để khi gõ phím, danh sách bên dưới KHÔNG bị thay đổi theo ngay lập tức.
            StudentName = student.StudentName;
            Email = student.Email;
            PhoneNumber = student.PhoneNumber;
            SelectedFacultyId = student.FacultyID;

            LoadFaculties();
        }

        private async void LoadFaculties()
        {
            var list = await _facultyService.GetAllFacultiesAsync();
            FacultyList.Clear();
            foreach (var item in list)
                FacultyList.Add(item);
        }

        [RelayCommand]
        private async Task Save()
        {
            // 1. Validate dữ liệu (nếu cần)
            if (string.IsNullOrWhiteSpace(StudentName))
                return;

            // 2. Cập nhật ngược lại vào Model gốc
            _originalStudent.StudentName = StudentName;
            _originalStudent.Email = Email;
            _originalStudent.PhoneNumber = PhoneNumber;
            _originalStudent.FacultyID = SelectedFacultyId;

            try
            {
                await _studentService.UpdateStudentAsync(_originalStudent);
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        [RelayCommand]
        private void Cancel()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
}