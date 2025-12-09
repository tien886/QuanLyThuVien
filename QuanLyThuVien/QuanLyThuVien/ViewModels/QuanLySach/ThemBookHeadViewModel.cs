using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class ThemBookHeadViewModel : ObservableObject
    {
        private readonly IBookService _bookService;
        private readonly IBookCategoryService _bookCategoryService;
        private readonly IGenreService _genreService;

        public ThemBookHeadViewModel(
            IBookService bookService,
            IBookCategoryService bookCategoryService,
            IGenreService genreService)
        {
            _bookService = bookService;
            _bookCategoryService = bookCategoryService;
            _genreService = genreService;

            // Tải dữ liệu cho ComboBox
            LoadGenres();
            LoadCategories();
        }

        // --- CÁC TRƯỜNG NHẬP LIỆU ---
        [ObservableProperty] 
        private string tenSach;
        [ObservableProperty] 
        private string tacGia;
        [ObservableProperty] 
        private string iSBN;
        [ObservableProperty] 
        private int namXB = DateTime.Now.Year; 
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


        private async void LoadGenres()
        {
            var list = await _genreService.GetAllGenresAsync();
            genres.Clear();
            foreach (var genre in list)
            {
                genres.Add(genre);
            }
            if (genres.Count > 0) 
                SelectedGenre = Genres[0];
        }

        private async void LoadCategories()
        {
            var list = await _bookCategoryService.GetAllBookCategoriesAsync();
            categories.Clear();
            foreach (var category in list)
            {
                categories.Add(category);
            }
            if (Categories.Count > 0) 
                SelectedCategory = categories[0];
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(TenSach))
            {
                MessageBox.Show("Vui lòng nhập tên sách!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(TacGia))
            {
                MessageBox.Show("Vui lòng nhập tác giả!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(ISBN))
            {
                MessageBox.Show("Vui lòng nhập ISBN!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // Kiểm tra độ dài ISBN (ví dụ 10 hoặc 13 số)
            if (ISBN.Length < 10 || ISBN.Length > 13)
            {
                MessageBox.Show("ISBN phải có độ dài 10 hoặc 13 ký tự!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (SelectedGenre == null || SelectedCategory == null)
            {
                MessageBox.Show("Vui lòng chọn Thể loại và Loại sách!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(NXB))
            {
                MessageBox.Show("Vui lòng nhập Nhà xuất bản!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Kiểm tra năm xuất bản hợp lý
            if (NamXB < 1900 || NamXB > DateTime.Now.Year + 1)
            {
                MessageBox.Show("Năm xuất bản không hợp lệ!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        [RelayCommand]
        public async Task AddBookHead()
        {
            if (!ValidateInput()) 
                return;

            var newBook = new Books
            {
                Title = TenSach,
                Author = TacGia,
                ISBN = ISBN,
                GenreID = SelectedGenre.GenreID,
                CategoryID = SelectedCategory.CategoryID,
                Publisher = NXB,
                PublicationYear = NamXB,
                Description = MoTa ?? string.Empty, 

                // Khởi tạo list rỗng để tránh null reference khi truy cập BookCopies sau này
                BookCopies = new List<BookCopies>()
            };

            try
            {
                // Lưu xuống Database (Chỉ lưu ID khóa ngoại)
                await _bookService.AddBookAsync(newBook);

                // Gán cái này mới cập nhật UI 
                newBook.Genre = SelectedGenre;
                newBook.BookCategory = SelectedCategory;

                WeakReferenceMessenger.Default.Send(new BookAddedMessage(newBook));

                MessageBox.Show("Thêm đầu sách thành công!", "Thông báo");
                ClosePopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message} \n {ex.InnerException?.Message}", "Lỗi Database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }

    // Class tin nhắn
    public class BookAddedMessage
    {
        public Books NewBook { get; }
        public BookAddedMessage(Books book) => NewBook = book;
    }
}