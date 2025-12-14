using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace QuanLyThuVien.ViewModels.QuanLyDanhMuc
{
    public partial class GenrePopupViewModel : ObservableObject
    {
        private readonly IGenreService _genreService;
        private readonly Genres _originalGenre; // Lưu bản gốc nếu đang sửa
        private readonly bool _isEditMode;

        public string Title => _isEditMode ? "Chỉnh sửa thể loại" : "Thêm thể loại mới";
        public string ButtonLabel => _isEditMode ? "Lưu thay đổi" : "Thêm mới";

        [ObservableProperty] private string _genreName;
        [ObservableProperty] private int _loanDurationDays;

        // Constructor
        public GenrePopupViewModel(IGenreService genreService, Genres genreToEdit = null)
        {
            _genreService = genreService;
            _originalGenre = genreToEdit;
            _isEditMode = (genreToEdit != null);

            if (_isEditMode)
            {
                // Load dữ liệu cũ
                GenreName = genreToEdit.GenreName;
                LoanDurationDays = genreToEdit.LoanDurationDays;
            }
            else
            {
                // Mặc định cho thêm mới
                LoanDurationDays = 14; // Ví dụ: mặc định mượn 2 tuần
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(GenreName))
            {
                MessageBox.Show("Vui lòng nhập tên thể loại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_isEditMode)
                {
                    // === LOGIC SỬA ===
                    _originalGenre.GenreName = GenreName;
                    _originalGenre.LoanDurationDays = LoanDurationDays;

                    await _genreService.UpdateGenreAsync(_originalGenre);
                    WeakReferenceMessenger.Default.Send(new GenreUpdatedMessage(_originalGenre));
                    MessageBox.Show("Cập nhật thành công!");
                }
                else
                {
                    // === LOGIC THÊM ===
                    var newGenre = new Genres
                    {
                        GenreName = GenreName,
                        LoanDurationDays = LoanDurationDays
                    };

                    await _genreService.AddGenreAsync(newGenre);
                    WeakReferenceMessenger.Default.Send(new GenreAddedMessage(newGenre));
                    MessageBox.Show("Thêm mới thành công!");
                }
                ClosePopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        [RelayCommand]
        private void IncreaseDays()
        {
            LoanDurationDays++;
        }

        [RelayCommand]
        private void DecreaseDays()
        {
            if (LoanDurationDays > 0)
                LoanDurationDays--;
        }

        [RelayCommand]
        private void ClosePopup() => WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
    }
}