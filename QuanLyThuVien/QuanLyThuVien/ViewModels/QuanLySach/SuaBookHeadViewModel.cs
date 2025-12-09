using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;

namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class SuaBookHeadViewModel : ObservableObject
    {
        private readonly IBookService _bookService;
        private readonly IBookCategoryService _bookCategoryService;
        private readonly IGenreService _genreService;
        private readonly Books _originalBook;

        // --- CÁC TRƯỜNG NHẬP LIỆU ---
        [ObservableProperty]
        private string tenSach;
        [ObservableProperty]
        private string tacGia;
        [ObservableProperty]
        private string iSBN;
        [ObservableProperty]
        private int namXB;
        [ObservableProperty]
        private string nXB;
        [ObservableProperty]
        private string moTa;

        // ComboBox
        [ObservableProperty]
        private Genres _selectedGenre;
        [ObservableProperty]
        private ObservableCollection<Genres> genres = new();
        [ObservableProperty]
        private BookCategories _selectedCategory;
        [ObservableProperty]
        private ObservableCollection<BookCategories> categories = new();

        public SuaBookHeadViewModel(
            Books book,
            IBookService bookService,
            IBookCategoryService bookCategoryService,
            IGenreService genreService
            )
        {
            _originalBook = book;
            _bookService = bookService;
            _bookCategoryService = bookCategoryService;
            _genreService = genreService;

            TenSach = book.Title;
            TacGia = book.Author;
            iSBN = book.ISBN;
            NamXB = book.PublicationYear;
            NXB = book.Publisher;
            MoTa = book.Description;
            SelectedGenre = book.Genre;
            SelectedCategory = book.BookCategory;
            LoadGenres();
            LoadCategories();
        }

        private async void LoadGenres()
        {
            var list = await _genreService.GetAllGenresAsync();
            genres.Clear();
            foreach ( var genre in list )
            {
                genres.Add(genre);
            }
        }

        private async void LoadCategories()
        {
            var list = await _bookCategoryService.GetAllBookCategoriesAsync();
            categories.Clear();
            foreach ( var category in list )
            {
                categories.Add(category);
            }
        }

        private bool ValidateInput()
        {
            return true;
        }

        [RelayCommand]
        public async Task Save()
        {
            if(!ValidateInput())
            {
                return;
            }

            // Cập nhật ngược lại vào model gốc (cái này là trong Book List - ObservableList)
            _originalBook.Title = TenSach;
            _originalBook.Author = TacGia;
            _originalBook.ISBN = iSBN;
            _originalBook.PublicationYear = NamXB;
            _originalBook.Publisher = NXB;
            _originalBook.Description = MoTa;
            _originalBook.GenreID = _selectedGenre.GenreID;
            _originalBook.CategoryID = _selectedCategory.CategoryID;

            var selectedGenreObj = Genres.FirstOrDefault(g => g.GenreID == _selectedGenre.GenreID);
            if (selectedGenreObj != null)
            {
                _originalBook.Genre = selectedGenreObj; 
            }

            var selectedCategoryObj = Categories.FirstOrDefault(c => c.CategoryID == _selectedCategory.CategoryID);
            if (selectedCategoryObj != null)
            {
                _originalBook.BookCategory = selectedCategoryObj; 
            }

            try
            {
                // Lưu thay đổi vào Database và phát tín hiệu cập nhật UI 
                await _bookService.UpdateBookAsync(_originalBook);
                WeakReferenceMessenger.Default.Send(new BookUpdatedMessage(_originalBook));
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

        public class BookUpdatedMessage
        {
            public Books UpdatedBook { get; }
            public BookUpdatedMessage(Books book) => UpdatedBook = book;
        }
    }
}
