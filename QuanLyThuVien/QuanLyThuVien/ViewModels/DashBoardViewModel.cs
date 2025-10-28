using CommunityToolkit.Mvvm.ComponentModel;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.ViewModels
{
    public partial class DashBoardViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBookCopyService _bookCopyService;
        private readonly IBookService _bookService;
        public DashBoardViewModel(
            IServiceProvider serviceProvider,
            IBookCopyService bookCopyService,
            IBookService bookService
            )
        {
            _serviceProvider = serviceProvider;
            _bookCopyService = bookCopyService;
            _bookService = bookService;
            _ = LoadPage();
        }
        [ObservableProperty]
        private int totalCopies;
        [ObservableProperty]
        private int borrowStudents;
        [ObservableProperty]
        private int totalBooks;
        [ObservableProperty]
        private int borrowedBooks;

        public async Task LoadPage()
        {
            TotalCopies = await _bookCopyService.GetTotalBookCopiesAsync();
            TotalBooks = await _bookService.GetTotalBooksAsync();
        }
    }
}
