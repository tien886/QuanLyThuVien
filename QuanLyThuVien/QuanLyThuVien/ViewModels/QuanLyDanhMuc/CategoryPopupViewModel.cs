// (Các using tương tự trên...)
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Windows;

namespace QuanLyThuVien.ViewModels.QuanLyDanhMuc
{
    public partial class CategoryPopupViewModel : ObservableObject
    {
        private readonly IBookCategoryService _service;
        private readonly BookCategories _original;
        private readonly bool _isEdit;

        public string Title => _isEdit ? "Sửa phân loại" : "Thêm phân loại";
        public string ButtonLabel => _isEdit ? "Lưu" : "Thêm";

        [ObservableProperty] private string _categoryName;

        public CategoryPopupViewModel(IBookCategoryService service, BookCategories item = null)
        {
            _service = service;
            _original = item;
            _isEdit = item != null;
            if (_isEdit) CategoryName = item.CategoryName;
        }

        [RelayCommand]
        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(CategoryName)) return;

            try
            {
                if (_isEdit)
                {
                    _original.CategoryName = CategoryName;
                    await _service.UpdateCategoryAsync(_original); // Giả sử service có hàm này
                    WeakReferenceMessenger.Default.Send(new CategoryUpdatedMessage(_original));
                }
                else
                {
                    var newItem = new BookCategories { CategoryName = CategoryName };
                    await _service.AddCategoryAsync(newItem); // Giả sử service có hàm này
                    WeakReferenceMessenger.Default.Send(new CategoryAddedMessage(newItem));
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