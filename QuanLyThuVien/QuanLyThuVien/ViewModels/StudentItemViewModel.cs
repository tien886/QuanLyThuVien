using CommunityToolkit.Mvvm.ComponentModel;
using QuanLyThuVien.Models; // <-- Using model
using System;

namespace QuanLyThuVien.ViewModels
{
    public partial class StudentItemViewModel : ObservableObject
    {
        private readonly Students _studentModel;
        public Students Student => _studentModel;

        public StudentItemViewModel(Students model)
        {
            _studentModel = model;
            _accountStatus = model.AccountStatus;
        }
        // Những thuộc tính chỉ đọc để hiển thị thông tin
        public int StudentId => _studentModel.StudentId;
        public string StudentName => _studentModel.StudentName;
        public string Email => _studentModel.Email;
        public string PhoneNumber => _studentModel.PhoneNumber;
        public DateTime RegistRegistrationDate => _studentModel.RegistrationDate;

        // Thuộc tính cần bind hai chiều với UI
        [ObservableProperty]
        private string _accountStatus; 

        // 5. (QUAN TRỌNG) Khi thuộc tính này thay đổi,
        //    hãy cập nhật lại model "ngốc" bên trong nó.
        partial void OnAccountStatusChanged(string value)
        {
            // Cập nhật model trên máy tính
            _studentModel.AccountStatus = value;
        }
    }
}