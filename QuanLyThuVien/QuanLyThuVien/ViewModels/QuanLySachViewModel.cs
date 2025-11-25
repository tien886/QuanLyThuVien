

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels.QuanLySach;
using QuanLyThuVien.ViewModels.QuanLySachPopup;
using QuanLyThuVien.Views;
using QuanLyThuVien.Views.QuanLySachPopup;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
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
                }
            }
        }

        [ObservableProperty]
        private ObservableCollection<Books> bookList = new();

        [ObservableProperty]
        private bool available = false;

        public async Task LoadPage()
        {
            BookList = new ObservableCollection<Books>(await _bookService.GetAllBooksAsync());
        }

        public async Task SearchBookAsync(string hint)
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                ObservableCollection<Books> searchedResults = [];
                foreach (Books book in books)
                {
                    string pseudoTitle = book.Title.ToLower();
                    string pseudoISBN = book.ISBN.ToLower();
                    string pseudoAuthor = book.Author.ToLower();
                    string pseudoHint = hint.ToLower();
                    if (pseudoTitle.Contains(pseudoHint) || pseudoISBN.Contains(pseudoHint) || pseudoAuthor.Contains(pseudoHint))
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
    } 
}
