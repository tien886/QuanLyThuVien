using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class ThemBookHeadViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBookService _bookService;
        private readonly IBookCategoryService _bookCategoryService;
        private readonly IGenreService _genreService;

        public ThemBookHeadViewModel(
            IServiceProvider serviceProvider,
            IBookService bookService,
            IBookCategoryService bookCategoryService, 
            IGenreService genreService
            )
        {
            _serviceProvider = serviceProvider;
            _bookService = bookService;
            _bookCategoryService = bookCategoryService;
            _genreService = genreService;
            Debug.WriteLine("HII in ThemBookHeadViewModel");
            _ = LoadData();
        }
        [ObservableProperty]
        private string tenSach;
        [ObservableProperty]
        private string tacGia;
        [ObservableProperty]
        private string iSBN;
        [ObservableProperty]
        private Genres loaiSachSelected;
        [ObservableProperty]
        private ObservableCollection<Genres> loaiSachs;
        [ObservableProperty]
        private BookCategories theLoaiSelected;
        [ObservableProperty]
        private ObservableCollection<BookCategories> theLoais;
        [ObservableProperty]
        private int namXB = 2025;
        [ObservableProperty]
        private string nXB;
        [ObservableProperty]
        private string moTa;
        public async Task LoadData()
        {
            var genres = await _genreService.GetAllGenresAsync();
            LoaiSachs = new ObservableCollection<Genres>(genres);
            var bookCategories = await _bookCategoryService.GetAllBookCategoriesAsync();
            TheLoais = new ObservableCollection<BookCategories>(bookCategories);
        }
        [RelayCommand]
        public async Task AddBookHead()
        {
            try
            {
                Books newBook = new Books
                {
                    Title = TenSach,
                    Author = TacGia,
                    ISBN = ISBN,
                    GenreID = LoaiSachSelected.GenreID,
                    CategoryID = TheLoaiSelected.CategoryID,
                    Publisher = NXB,
                    PublicationYear = NamXB,
                    Description = MoTa
                };
                Debug.WriteLine(newBook.Title);
                Debug.WriteLine(newBook.Author);
                Debug.WriteLine(newBook.ISBN);
                Debug.WriteLine(newBook.GenreID);
                Debug.WriteLine(newBook.Publisher);
                Debug.WriteLine(newBook.PublicationYear);
                Debug.WriteLine(newBook.CategoryID);
                Debug.WriteLine(newBook.Description);

                if (CheckValidBookHead(newBook))
                {
                    System.Windows.MessageBox.Show("Thêm đầu sách thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    await _bookService.AddBookAsync(newBook);
                    await ClosePopup();
                    Debug.WriteLine($"Added book head: {newBook.Title}");
                }
                else
                    System.Windows.MessageBox.Show("Thông tin đầu sách không hợp lệ! Vui lòng kiểm tra lại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show($"Error 500: {ex}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool CheckValidBookHead(Books book)
        {
            if (book == null)
            {
                Debug.WriteLine("[ERROR] Book is NULL");
                return false;
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                Debug.WriteLine("[INVALID] Title is empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(book.Author))
            {
                Debug.WriteLine("[INVALID] Author is empty");
                return false;
            }
            if(book.GenreID == 0 || book.CategoryID == 0)
            {
                Debug.WriteLine("[INVALID] Not choose genre or category");
                return false;
            }
            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                Debug.WriteLine("[INVALID] ISBN is empty");
                return false;
            }
            if (book.ISBN.Length != 13)
            {
                Debug.WriteLine($"[INVALID] ISBN length = {book.ISBN.Length}, expected >= 13");
                return false;
            }

            if (string.IsNullOrWhiteSpace(book.Publisher))
            {
                Debug.WriteLine("[INVALID] Publisher is empty");
                return false;
            }

            // Your condition was incorrect. Now logging clearly:
            if (book.PublicationYear < 1980 || book.PublicationYear > 2026)
            {
                Debug.WriteLine($"[INVALID] PublicationYear = {book.PublicationYear}, valid range = 1980–2026");
                return false;
            }

            Debug.WriteLine("[PASS] BookHead is valid");
            return true;
        }

        [RelayCommand]
        public async Task ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
}

