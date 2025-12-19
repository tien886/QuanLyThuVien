using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels.QuanLySach;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using static QuanLyThuVien.ViewModels.QuanLySach.SuaBookHeadViewModel;
namespace QuanLyThuVien.ViewModels
{
    public partial class QuanLySachViewModel : ObservableObject, IHeaderActionViewModel
    {
        // Các service 
        private readonly IBookService _bookService;
        private readonly IBookCopyService _bookCopyService;
        private readonly IGenreService _genreService;
        private readonly IBookCategoryService _bookCategoryService;
        private readonly ILocationService _locationService;

        // Các thuộc tính cho IHeaderActionViewModel
        public ICommand HeaderButtonCommand => OpenThemBookHeadPopupCommand;
        public string HeaderButtonLabel => "Thêm đầu sách";
        public bool IsHeaderButtonVisible => true;

        // Danh sách sách hiển thị
        [ObservableProperty]
        private ObservableCollection<BookItemViewModel> _bookList = new();

        // Thuộc tính phân trang
        private int PageSize = 15;
        private const int WindowSize = 5;
        [ObservableProperty]
        private int currentPage;
        [ObservableProperty]
        private int totalPages;
        [ObservableProperty] private int firstPage;
        [ObservableProperty] private int secondPage;
        [ObservableProperty] private int thirdPage;
        [ObservableProperty] private int fourthPage;
        [ObservableProperty] private int fifthPage;

        // Thuộc tính tìm kiếm
        [ObservableProperty]
        private string _searchText = string.Empty;
        private CancellationTokenSource? _searchCts;


        public QuanLySachViewModel(
            IBookService bookService,
            IBookCopyService bookCopyService,
            IGenreService genreService,
            IBookCategoryService bookCategoryService,
            ILocationService locationService
            )
        {
            _bookService = bookService;
            _bookCopyService = bookCopyService;
            _genreService = genreService;
            _bookCategoryService = bookCategoryService;
            _locationService = locationService;
            _ = LoadAsync();

            WeakReferenceMessenger.Default.Register<BookUpdatedMessage>(this, (r, message) =>
            {
                var bookItemVM = BookList.FirstOrDefault(bookItem => bookItem.MaDauSach == message.UpdatedBook.BookID);
                if (bookItemVM != null)
                {
                    bookItemVM.RefreshFromModel(message.UpdatedBook);
                }
            });

            WeakReferenceMessenger.Default.Register<BookAddedMessage>(this, (r, message) =>
            {
                var newBookItemVM = new BookItemViewModel(message.NewBook);
                BookList.Add(newBookItemVM);
            });

            WeakReferenceMessenger.Default.Register<BookCopyAddedMessage>(this, HandleBookCopyAdded);

            _ = LoadAsync();
        }

        // Thao tác tìm kiếm
        [RelayCommand]
        private async Task LoadAsync()
        {
            try
            {
                CurrentPage = 1;
                // Truyền SearchText xuống Service để tính tổng số trang dựa trên kết quả tìm kiếm
                TotalPages = await _bookService.GetTotalPages(PageSize, SearchText);
                UpdateWindow();
                await LoadCurrentPageAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi LoadAsync: {ex.Message}");
            }
        }

        async partial void OnSearchTextChanged(string value)
        {
            await SearchAsync();
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            _searchCts?.Cancel(); // Hủy lệnh tìm kiếm trước đó nếu đang chạy
            _searchCts = new CancellationTokenSource();
            var token = _searchCts.Token;

            try
            {
                await Task.Delay(350, token); // Đợi 350ms ngưng gõ mới tìm
                await LoadAsync(); // Gọi lại hàm LoadAsync (nó sẽ lấy SearchText mới nhất)
            }
            catch (TaskCanceledException) { }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private async Task LoadCurrentPageAsync()
        {
            if (TotalPages == 0)
            {
                BookList.Clear();
                return;
            }

            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages) CurrentPage = TotalPages;

            // Truyền SearchText xuống để lấy đúng dữ liệu trang đang tìm
            var books = await _bookService.GetBooksPage(CurrentPage - 1, PageSize, SearchText);

            BookList.Clear();
            foreach (var book in books)
            {
                BookList.Add(new BookItemViewModel(book));
            }
        }

        [ObservableProperty]
        private bool available = false;


        // Các thao tác chỉnh sửa, thêm, xóa, xem chi tiết
        [RelayCommand]
        private async Task DeleteBook(BookItemViewModel bookVM)
        {
            if (bookVM == null) return;

            var result = System.Windows.MessageBox.Show(
                $"Bạn có chắc muốn xóa sách \"{bookVM.TenSach}\" không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            await _bookService.DeleteBookAsync(bookVM.Book);  // Xóa khỏi database
            BookList.Remove(bookVM); // Xóa khỏi danh sách hiển thị
        }

        [RelayCommand]
        private async Task OpenBookDetailAndCopyPopup(BookItemViewModel bookVM)
        {
            var bookDetailAndCopyVM = new BookDetailAndCopyViewModel(bookVM.Book, _bookCopyService, _locationService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(bookDetailAndCopyVM));
        }

        [RelayCommand]
        public async Task OpenThemBookHeadPopup()
        {
            var addBookHeadVM = new ThemBookHeadViewModel(_bookService, _bookCategoryService, _genreService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(addBookHeadVM));
        }

        [RelayCommand]
        public async Task OpenSuaBookHeadPopup(BookItemViewModel bookVM)
        {
            var suaBookHeadVM = new SuaBookHeadViewModel(bookVM.Book, _bookService, _bookCategoryService, _genreService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(suaBookHeadVM));
        }


        // Thao tác phân trang
        [RelayCommand]
        private void PageSelection(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > TotalPages) return;
            CurrentPage = pageNumber;
            UpdateWindow();
            _ = LoadCurrentPageAsync();
        }

        [RelayCommand]
        private async Task GoToPreviousPage()
        {
            if (CurrentPage <= 1) return;
            CurrentPage--;
            UpdateWindow();
            await LoadCurrentPageAsync();
        }

        [RelayCommand]
        private async Task GoToNextPage()
        {
            if (CurrentPage >= TotalPages) return;
            CurrentPage++;
            UpdateWindow();
            await LoadCurrentPageAsync();
        }
        private void UpdateWindow()
        {
            if (TotalPages <= 1)
                return;

            int start = CurrentPage - WindowSize / 2;
            if (start < 1)
                start = 1;

            int end = start + WindowSize - 1;
            if (end > TotalPages)
            {
                end = TotalPages;
                start = Math.Max(1, end - WindowSize + 1);
            }

            FirstPage = start;
            SecondPage = start + 1 <= end ? start + 1 : 0;
            ThirdPage = start + 2 <= end ? start + 2 : 0;
            FourthPage = start + 3 <= end ? start + 3 : 0;
            FifthPage = start + 4 <= end ? start + 4 : 0;
        }

        // Xử lý khi có bản sao sách mới được thêm
        private async void HandleBookCopyAdded(object recipient, BookCopyAddedMessage message)
        {
            var targetBookItem = BookList.FirstOrDefault(item => item.MaDauSach == message.NewCopy.BookID);

            if (targetBookItem != null)
            {
                targetBookItem.UpdateCountsWhenCopyAdded(message.NewCopy);
            }
        }
    }
}
