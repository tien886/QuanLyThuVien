using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels.QuanLySach;
using QuanLyThuVien.ViewModels.QuanLySachPopup;
using QuanLyThuVien.Views.QuanLySachPopup;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
namespace QuanLyThuVien.ViewModels
{
    public partial class QuanLySachViewModel : ObservableObject
    {
        private IServiceProvider _serviceProvider;
        private IBookService _bookService;
        public QuanLySachViewModel(
            IServiceProvider serviceProvider,
            IBookService bookService
            )
        {
            _serviceProvider = serviceProvider;
            _bookService = bookService;
            _ = LoadPage();
        }
        private async Task LoadCurrentPageAsync()
        {
            if (CurrentPage < 1 || CurrentPage > TotalPages)
                return;
            var books = await _bookService.GetBooksPage(CurrentPage - 1, PageSize);
            BookList = new ObservableCollection<Books>(books);
        }
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
        private string _searhBarText = "";
        public string SearchBarText
        {
            get => _searhBarText;
            set
            {
                if (_searhBarText != value)
                {
                    _searhBarText = value;
                    OnPropertyChanged();
                    _ = SearchBookAsync(SearchBarText);
                    Task.Delay(1000);
                }
            }
        }

        [ObservableProperty]
        private ObservableCollection<Books> bookList = new();

        [ObservableProperty]
        private bool available = false;
        // Pagination
        public async Task LoadPage()
        {
            CurrentPage = 1;
            TotalPages = await _bookService.GetTotalPages(PageSize);
            UpdateWindow();
            await LoadCurrentPageAsync();
            Debug.WriteLine(CurrentPage);
            Debug.WriteLine(TotalPages);
        }
        
        public async Task SearchBookAsync(string hint)
        {
            try
            {
                if(hint == "")
                {
                    await LoadCurrentPageAsync();
                    return;
                }
                var books = await _bookService.GetAllBooksAsync();
                ObservableCollection<Books> searchedResults = [];
                foreach (Books book in books)
                {
                    string pseudoTitle = book.Title.ToLower();
                    string pseudoISBN = book.ISBN.ToLower();
                    string pseudoAuthor = book.Author.ToLower();
                    string pseudoCategoryName = book.BookCategory.CategoryName.ToLower();
                    string pseudoHint = hint.ToLower();
                    if (pseudoTitle.Contains(pseudoHint) || pseudoISBN.Contains(pseudoHint) || pseudoAuthor.Contains(pseudoHint) || pseudoCategoryName.Contains(pseudoHint))
                        searchedResults.Add(book);
                }
                BookList = searchedResults;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi tìm kiếm: {ex.Message}");
            }
        }
        

        [RelayCommand]
        private async Task DeleteBook(Books book)
        {
            if (book == null) return;

            var result = System.Windows.MessageBox.Show(
                $"Bạn có chắc muốn xóa sách \"{book.Title}\" không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            await _bookService.DeleteBookAsync(book);
            BookList.Remove(book);
        }
        [RelayCommand]
        public async Task OpenBookDetailAndCopyPopup(Books book)
        {

            var bookDetailAndCopyWindow = _serviceProvider.GetRequiredService<BookDetailAndCopyPopup>();
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(bookDetailAndCopyWindow));
            if (bookDetailAndCopyWindow.DataContext is BookDetailAndCopyViewModel vm)
            {
                await vm.LoadPage(book);
            }
        }
        [RelayCommand]
        public async Task OpenThemBookHeadPopup()
        {
            var themBookHeadPopup = _serviceProvider.GetRequiredService<ThemBooKHeadPopup>();
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(themBookHeadPopup));
        }
        [RelayCommand]
        public async Task OpenSuaBookHeadPopup(Books book)
        {
            var suaBookHeadPopup = _serviceProvider.GetRequiredService<SuaBookHeadPopup>();
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(suaBookHeadPopup));
            if (suaBookHeadPopup.DataContext is SuaBookHeadViewModel vm)
            {
                await vm.LoadPage(book);
            }
        }
        [RelayCommand]
        private void PageSelection(int pageNumber)
        {
            Debug.WriteLine(pageNumber);
            if (pageNumber < 0 || pageNumber > TotalPages)
                return;

            CurrentPage = pageNumber;
            UpdateWindow();
            _ = LoadCurrentPageAsync();
        }
        [RelayCommand]
        private async Task GoToPreviousPage()
        {
            if (CurrentPage <= 1)
                return;

            CurrentPage--;
            UpdateWindow();
            Debug.WriteLine(CurrentPage);

            await LoadCurrentPageAsync();
        }

        [RelayCommand]
        private async Task GoToNextPage()
        {
            if (CurrentPage >= TotalPages)
                return;

            CurrentPage++;
            UpdateWindow();
            Debug.WriteLine(CurrentPage);

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
    } 
}
