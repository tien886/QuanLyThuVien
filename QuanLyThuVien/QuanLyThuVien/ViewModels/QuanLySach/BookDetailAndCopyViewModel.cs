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
namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class BookDetailAndCopyViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBookCopyService _bookCopyService;
        private Books currentBook;
        public BookDetailAndCopyViewModel(
            IServiceProvider serviceProvider,
            IBookCopyService bookService
            )
        {
            _serviceProvider = serviceProvider;
            _bookCopyService = bookService;

        }

        [ObservableProperty]
        private int maDauSach;
        [ObservableProperty]
        private string tenSach;
        [ObservableProperty]
        private string tacGia;
        [ObservableProperty]
        private string iSBN;
        [ObservableProperty]
        private string theLoai;
        [ObservableProperty]
        private string nXB;
        [ObservableProperty]
        private string namXB;
        [ObservableProperty]
        private string moTa;
        [ObservableProperty]
        private int tongSoBan;
        [ObservableProperty]
        private int coSan;
        [ObservableProperty]
        private int dangMuon;
        [ObservableProperty]
        private int hongMat;
        [ObservableProperty]
        private ObservableCollection<BookCopies> bookCopiesList = new();

        public async Task LoadPage(Books book)
        {
            currentBook = book;
            Debug.WriteLine($"Go to loadpage");
            try
            {
                Debug.WriteLine($"book is ok");
                MaDauSach = currentBook.BookID;
                TenSach = currentBook.Title;
                TacGia = currentBook.Author;
                ISBN = currentBook.ISBN;
                TheLoai = currentBook.BookCategory.CategoryName;
                NXB = currentBook.Publisher;
                NamXB = currentBook.PublicationYear.ToString();
                MoTa = currentBook.Description;
                // badget show up
                TongSoBan = currentBook.TotalCopies;
                CoSan = currentBook.AvailableCount;
                DangMuon = currentBook.BookCopies.Count(c => c.Status == "0");
                HongMat = TongSoBan - CoSan - DangMuon;
                // copies list 
                BookCopiesList = new ObservableCollection<BookCopies>(await _bookCopyService.GetAllBookCopiesAsync(book));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi tải trang: {ex.Message}");
            }
            ;
        }
        [RelayCommand]
        public async Task ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
        [RelayCommand]
        public async Task AddCopies()
        {
            var themBookCopyPopup = _serviceProvider.GetRequiredService<ThemBookCopyPopup>();
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(themBookCopyPopup));
            if(themBookCopyPopup.DataContext is ThemBookCopyViewModel vm)
            {
                await vm.SetCurrentBook(currentBook);
            }
            await LoadPage(currentBook);
        }
        [RelayCommand]
        public async Task OpenSuaBookCopyPopup(BookCopies bookCopy)
        {
            var suaBookCopyPopup = _serviceProvider.GetRequiredService<SuaBookCopyPopup>();
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(suaBookCopyPopup));
            if (suaBookCopyPopup.DataContext is SuaBookCopyViewModel vm)
            {
                await vm.SetCurrentBookCopy(bookCopy);
            }
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
    }
}
