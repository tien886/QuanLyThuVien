using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using QuanLyThuVien.Views.QuanLySachPopup;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class BookDetailAndCopyViewModel : ObservableObject
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly ILocationService _locationService;
        private readonly Books _book;

        [ObservableProperty]
        private int _maDauSach;
        [ObservableProperty]
        private string _tenSach;
        [ObservableProperty]
        private string _tacGia;
        [ObservableProperty]
        private string _iSBN;
        [ObservableProperty]
        private string _theLoai;
        [ObservableProperty]
        private string _nXB;
        [ObservableProperty]
        private string _namXB;
        [ObservableProperty]
        private string _moTa;
        [ObservableProperty]
        private int _tongSoBan;
        [ObservableProperty]
        private int _coSan;
        [ObservableProperty]
        private int _dangMuon;
        [ObservableProperty]
        private int _hongMat;
        [ObservableProperty]
        private ObservableCollection<BookCopies> _bookCopiesList = new();

        public BookDetailAndCopyViewModel(
            Books book,
            IBookCopyService bookService,
            ILocationService locationService
            )
        {
            _book = book;
            _bookCopyService = bookService;
            _locationService = locationService;

            MaDauSach = book.BookID;
            TenSach = book.Title;
            TacGia = book.Author;
            ISBN = book.ISBN;
            TheLoai = book.BookCategory.CategoryName;
            NXB = book.Publisher;
            NamXB = book.PublicationYear.ToString();
            MoTa = book.Description;
            TongSoBan = book.TotalCopies;
            CoSan = book.AvailableCount;
            DangMuon = book.BookCopies.Count(c => c.Status == "0");
            HongMat = _tongSoBan - _coSan - _dangMuon;
            LoadBookCopies();
        }

        private async void LoadBookCopies()
        {
            var list = _bookCopyService.GetAllBookCopiesAsync(_book);
            BookCopiesList.Clear();
            foreach (var bookCopy in await list)
            {
                BookCopiesList.Add(bookCopy);
            }
        }

        [RelayCommand]
        private void OpenThemBookCopyPopup()
        {
            var addBookCopyVM = new ThemBookCopyViewModel(_book, _bookCopyService, _locationService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(addBookCopyVM));
        }

        [RelayCommand]
        public async Task OpenSuaBookCopyPopup(BookCopies bookCopy)
        {
            var suaBookCopyVM = new SuaBookCopyViewModel(bookCopy, _bookCopyService, _locationService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(suaBookCopyVM));
        }

        [RelayCommand]
        public async Task DeleteBookCopy(BookCopies bookCopy)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa bản sao {bookCopy.CopyID} không?",
                "Cảnh báo",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                if(bookCopy != null)
                    await _bookCopyService.DeleteCopiesAsync(bookCopy);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa bản sao: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
}
