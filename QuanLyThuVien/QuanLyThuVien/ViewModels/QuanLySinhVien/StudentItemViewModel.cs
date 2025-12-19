using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.ViewModels
{
    public partial class StudentItemViewModel : ObservableObject
    {
        // Giữ tham chiếu model gốc để dùng khi cần (ví dụ khi nhấn Sửa)
        private readonly Students _studentModel;
        public StudentItemViewModel(Students model)
        {
            _studentModel = model;

            // Khởi tạo giá trị ban đầu từ Model lên View
            StudentName = model.StudentName;
            Email = model.Email;
            PhoneNumber = model.PhoneNumber;
            FacultyName = model.Faculty?.FacultyName ?? "Chưa cập nhật";
            AccountStatus = model.AccountStatus;
        }

        // --- Các thuộc tính binding 2 chiều ---

        [ObservableProperty]
        private string _studentName;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _phoneNumber;

        [ObservableProperty]
        private string _facultyName;

        [ObservableProperty]
        private string _accountStatus;

        // --- Các thuộc tính readonly ---
        public Students Student => _studentModel;
        public int StudentId => _studentModel.StudentId;
        public DateTime RegistrationDate => _studentModel.RegistrationDate;

        // --- Các thuộc tính thống kê : Được cha bơm cho ---
        [ObservableProperty]
        private int _tongDaMuon;

        [ObservableProperty]
        private int _dangMuon;

        [ObservableProperty]
        private int _quaHan;

        public void RefreshFromModel(Students updatedModel)
        {
            // Cập nhật lên Properties để UI tự động thay đổi (NotifyPropertyChanged)
            StudentName = updatedModel.StudentName;
            Email = updatedModel.Email;
            PhoneNumber = updatedModel.PhoneNumber;
            FacultyName = updatedModel.Faculty?.FacultyName ?? "Chưa cập nhật";
            AccountStatus = updatedModel.AccountStatus;
        }
        [RelayCommand]
        private void ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
}