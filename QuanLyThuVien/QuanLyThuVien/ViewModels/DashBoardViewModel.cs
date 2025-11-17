using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.ViewModels
{
    public partial class DashBoardViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBookCopyService _bookCopyService;
        private readonly IBookService _bookService;
        private readonly IStudentService _studentService;
        public DashBoardViewModel(
            IServiceProvider serviceProvider,
            IBookCopyService bookCopyService,
            IBookService bookService,
            IStudentService studentService
            )
        {
            _serviceProvider = serviceProvider;
            _bookCopyService = bookCopyService;
            _bookService = bookService;
            _studentService = studentService;
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

        // Thuộc tính cho PieChart Phân bổ trạng thái sách
        [ObservableProperty] 
        private ISeries[] _pieChartSeries;

        // Ba thuộc tính cho AreaChart Bạn đọc mới đăng ký
        [ObservableProperty]
        private ISeries[] _areaChartSeries;

        [ObservableProperty]
        private Axis[] _areaChartXAxes;

        [ObservableProperty]
        private Axis[] _areaChartYAxes;


        public async Task LoadPage()
        {
            TotalCopies = await _bookCopyService.GetTotalBookCopiesAsync();
            TotalBooks = await _bookService.GetTotalBooksAsync();
            _pieChartSeries = await _bookCopyService.GetBookStatusData();
            try
            {
                var (series, xAxes) = await _studentService.GetNewReadersData();
                AreaChartSeries = series;
                AreaChartXAxes = xAxes;
            }
            catch (Exception ex)
            { 
                Console.WriteLine("Lỗi:" + ex.Message);
            }
            

        }
    }
}
