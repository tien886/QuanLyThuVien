using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace QuanLyThuVien.ViewModels
{
    public partial class SuaStudentViewModel : ObservableObject
    {
        private readonly IStudentService _studentService;
        private readonly IFacultyService _facultyService;
        private readonly Students _originalStudent; // Giữ tham chiếu gốc để update sau khi Save 

        [ObservableProperty]
        private string _studentName;
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _phoneNumber;
        [ObservableProperty]
        private string _faculty;
        [ObservableProperty]
        private int _selectedFacultyId;
        public ObservableCollection<Faculties> FacultyList { get; } = new();
        public string StudentId => _originalStudent.StudentId.ToString();

        public SuaStudentViewModel(Students student,
            IStudentService studentService,
            IFacultyService facultyService)
        {
            _studentService = studentService;
            _facultyService = facultyService;
            _originalStudent = student;

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

            if (FacultyList.Count > 0)
            {
                SelectedFacultyId = FacultyList[0].FacultyID;
            }
        }

        private bool ValidateInput()
        {
            // 1.1 Kiểm tra Họ và tên
            if (string.IsNullOrWhiteSpace(StudentName))
            {
                MessageBox.Show("Vui lòng nhập họ và tên sinh viên!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // 1.2 Kiểm tra Số điện thoại (Phải là số, 10-11 ký tự, bắt đầu bằng số 0)
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // Logic: Phải là số VÀ (độ dài 10 hoặc 11) VÀ (bắt đầu bằng '0')
            if (!PhoneNumber.All(char.IsDigit) ||
                (PhoneNumber.Length != 10 && PhoneNumber.Length != 11) ||
                !PhoneNumber.StartsWith("0"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ (Phải là 10-11 số và bắt đầu bằng số 0).", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // 1.3 Kiểm tra Email (Dùng Regex chuẩn)
            if (string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Vui lòng nhập Email!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(Email, emailPattern))
            {
                MessageBox.Show("Định dạng Email không hợp lệ!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        [RelayCommand]
        private async Task Save()
        {
            if (!ValidateInput())
            {
                return;
            }

            //// Cập nhật ngược lại vào model gốc (cái này là trong Student List - ObservableList)
            _originalStudent.StudentName = StudentName;
            _originalStudent.Email = Email;
            _originalStudent.PhoneNumber = PhoneNumber;
            _originalStudent.FacultyID = SelectedFacultyId;
            var selectedFacultyObj = FacultyList.FirstOrDefault(f => f.FacultyID == SelectedFacultyId);

            if (selectedFacultyObj != null)
            {
                _originalStudent.Faculty = selectedFacultyObj;
            }

            try
            {
                // Lưu thay đổi vào Database và phát tín hiệu cập nhật UI 
                await _studentService.UpdateStudentAsync(_originalStudent);
                WeakReferenceMessenger.Default.Send(new StudentUpdatedMessage(_originalStudent));
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                ClosePopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        [RelayCommand]
        private void ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }

    public class StudentUpdatedMessage
    {
        public Students UpdatedStudent { get; }
        public StudentUpdatedMessage(Students student) => UpdatedStudent = student;
    }
}