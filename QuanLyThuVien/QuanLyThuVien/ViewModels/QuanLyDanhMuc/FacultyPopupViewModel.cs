using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Windows;

namespace QuanLyThuVien.ViewModels.QuanLyDanhMuc
{
    public partial class FacultyPopupViewModel : ObservableObject
    {
        private readonly IFacultyService _service;
        private readonly Faculties _original;
        private readonly bool _isEdit;

        public string Title => _isEdit ? "Sửa tên khoa" : "Thêm khoa mới";
        public string ButtonLabel => _isEdit ? "Lưu thay đổi" : "Thêm mới";

        [ObservableProperty] private string _facultyName;

        public FacultyPopupViewModel(IFacultyService service, Faculties item = null)
        {
            _service = service;
            _original = item;
            _isEdit = item != null;
            if (_isEdit) FacultyName = item.FacultyName;
        }

        [RelayCommand]
        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(FacultyName)) return;

            try
            {
                if (_isEdit)
                {
                    _original.FacultyName = FacultyName;
                    await _service.UpdateFacultyAsync(_original);
                    WeakReferenceMessenger.Default.Send(new FacultyUpdatedMessage(_original));
                }
                else
                {
                    var newItem = new Faculties { FacultyName = FacultyName };
                    await _service.AddFacultyAsync(newItem);
                    WeakReferenceMessenger.Default.Send(new FacultyAddedMessage(newItem));
                }
                ClosePopup();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message); 
            }
        }

        [RelayCommand]
        private void ClosePopup() => WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
    }
}