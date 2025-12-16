using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Services;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyThuVien.ViewModels
{
    public partial class DashBoardViewModel : ObservableObject, IHeaderActionViewModel
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly IBookService _bookService;
        private readonly IStudentService _studentService;
        private readonly ILoanService _loanService;
        private readonly IReportExportService _reportExportService;

        // Implement Interface cho nút Header
        public bool IsHeaderButtonVisible => true;
        public string HeaderButtonLabel => "Xuất báo cáo";
        public ICommand HeaderButtonCommand => OpenExportPopupCommand;

        [RelayCommand]
        private void OpenExportPopup()
        {
            // Mở popup chọn định dạng
            var vm = new ExportReportViewModel(_reportExportService, loantrendstats, categoryloanstats, monthlyreaderstats, bookstatusstats);
            // Nếu logic xuất file cần dữ liệu từ Dashboard, bạn có thể truyền DashboardVM vào đây
            // var vm = new ExportReportViewModel(this.Stats, this.ChartData...);

            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(vm));
        }

        // Các thuộc tính để hiển thị 
        [ObservableProperty]
        private int _totalCopies;
        [ObservableProperty]
        private int _borrowStudents;
        [ObservableProperty]
        private int _totalBooks;
        [ObservableProperty]
        private int _borrowedBooks;
        [ObservableProperty]
        private int _overdueBooks;

        // Thuộc tính cho PieChart Phân bổ trạng thái sách
        [ObservableProperty]
        private ISeries[] _pieChartSeries;

        // Ba thuộc tính cho AreaChart Bạn đọc mới đăng ký
        private const int NumberOfMonthsForAreaChart = 11; // Số tháng để hiển thị dữ liệu
        [ObservableProperty]
        private ISeries[] _areaChartSeries;
        [ObservableProperty]
        private Axis[] _areaChartXAxes;
        [ObservableProperty]
        private Axis[] _areaChartYAxes;

        // Các thuộc tính cho LineChart xu hướng mượn sách 
        private const int NumberOfMonthsForLineChart = 11; // Số tháng để hiển thị xu hướng
        [ObservableProperty]
        private ISeries[] _lineChartSeries;
        [ObservableProperty]
        private Axis[] _lineChartXAxes;

        // Ba thuộc tính cho BarChart xu hướng mượn sách
        private const int TopBookCategories = 7; // Số thể loại sách phổ biến nhất để hiển thị
        [ObservableProperty]
        private ISeries[] _barChartSeries;
        [ObservableProperty]
        private Axis[] _barChartXAxes;
        [ObservableProperty]
        private Axis[] _barChartYAxes;

        // Thuộc tính chứa danh sách Sách được mượn nhiều nhất
        private const int TopBooksCount = 5; // Số sách phổ biến nhất để hiển thị
        [ObservableProperty]
        private IEnumerable<BookLoanStats> _topBooksList;

        // Thuộc tính chứa các hoạt động gần đây
        public int NumberOfRecentActivities { get; } = 8;
        [ObservableProperty]
        private ObservableCollection<ActivityDisplayItem> _recentActivitiesList;

        // Thuộc tính chứa các sách quá hạn 
        private const int OverdueBooksCount = 5;
        [ObservableProperty]
        private IEnumerable<OverdueBookStats> _overdueBooksList;
        // these properties use for exporting file
        private IEnumerable<LoanTrendStats> loantrendstats;
        private IEnumerable<MonthlyReaderStats> monthlyreaderstats;
        private IEnumerable<CategoryLoanStats> categoryloanstats;
        private BookStatusStats bookstatusstats;
        public DashBoardViewModel(
            IBookCopyService bookCopyService,
            IBookService bookService,
            IStudentService studentService,
            ILoanService loanService,
            IReportExportService reportExportService
            )
        {
            _bookCopyService = bookCopyService;
            _bookService = bookService;
            _studentService = studentService;
            _loanService = loanService;
            _reportExportService = reportExportService;
            _ = LoadPage();
        }

        public async Task LoadPage()
        {
            // 1.Load các số liệu đơn cho badges - Tổng số sách, Sách đang mượn, Bạn đọc đang mượn, Sách quá hạn
            TotalCopies = await _bookCopyService.GetTotalBookCopiesAsync();
            TotalBooks = await _bookService.GetTotalBooksAsync();
            BorrowedBooks = await _loanService.GetCurrentlyBorrowedBooksAsync();
            BorrowStudents = await _loanService.GetCurrentBorrowingStudentsAsync();
            OverdueBooks = await _loanService.GetOverdueBooksAsyncCount();

            // 2. Load và tạo biểu đồ Tròn - Phân bổ trạng thái sách
            var statusStats = await _bookCopyService.GetBookStatusStatsAsync();
            bookstatusstats = statusStats;
            PieChartSeries = CreatePieChartData(statusStats);

            // 3. Load và tạo biểu đồ Vùng - Bạn đọc mới
            var startDateAreaChart = DateTime.Now.AddMonths(-NumberOfMonthsForAreaChart);
            var endDateAreaChart = DateTime.Now;
            var readerStats = await _studentService.GetNewReadersStatsAsync(startDateAreaChart, endDateAreaChart);
            monthlyreaderstats = readerStats;
            CreateAreaChartData(readerStats);

            // 4. Load và tạo biểu đồ Đường - Xu hướng mượn trả sách
            var startDateLineChart = DateTime.Now.AddMonths(-NumberOfMonthsForLineChart);
            var endDateLineChart = DateTime.Now;
            var trendsData = await _loanService.GetLoanTrendsAsync(startDateLineChart, endDateLineChart);
            loantrendstats = trendsData;
            CreateLineChartData(trendsData);

            // 5. Load và tạo biểu đồ Cột - Thể loại sách phổ biến
            var categoryStats = await _loanService.GetLoanStatsByCategoryAsync(TopBookCategories);
            categoryloanstats = categoryStats;
            CreateBarChartData(categoryStats);

            // 6. Load Top sách mượn nhiều nhất
            TopBooksList = await _loanService.GetTopBorrowedBooksAsync(TopBooksCount);

            // 7. Load Hoạt động gần đây 
            await LoadRecentActivities();
            // 8. Load Sách quá hạn
            OverdueBooksList = await _loanService.GetOverdueBooksAsync(OverdueBooksCount);
        }

        private ISeries[] CreatePieChartData(BookStatusStats stats)
        {
            return new ISeries[]
            {
                new PieSeries<int> { Values = new[] { stats.CoSan }, Name = "Có sẵn", InnerRadius = 50, Fill = new SolidColorPaint(SKColor.Parse("#10B981")) },
                new PieSeries<int> { Values = new[] { stats.DangMuon }, Name = "Đang mượn", InnerRadius = 50, Fill = new SolidColorPaint(SKColor.Parse("#0EA5E9")) },
                new PieSeries<int> { Values = new[] { stats.Hong }, Name = "Hỏng", InnerRadius = 50, Fill = new SolidColorPaint(SKColor.Parse("#F59E0B")) },
                new PieSeries<int> { Values = new[] { stats.Mat }, Name = "Mất", InnerRadius = 50, Fill = new SolidColorPaint(SKColor.Parse("#EF4444")) }
            };
        }
        private void CreateAreaChartData(IEnumerable<MonthlyReaderStats> stats)
        {
            var counts = new List<int>();
            var labels = new List<string>();

            foreach (var item in stats)
            {
                counts.Add(item.Count);
                // Format ngày tháng là việc của UI/ViewModel
                labels.Add($"T{item.Month}/{item.Year.ToString().Substring(2)}");
            }

            AreaChartSeries = new ISeries[]
            {
                new LineSeries<int>
                {
                    Values = counts.ToArray(),
                    Name = "Bạn đọc mới",
                    Stroke = new SolidColorPaint(SKColor.Parse("#10B981")) { StrokeThickness = 3 },
                    Fill = new SolidColorPaint(SKColor.Parse("#10B981").WithAlpha(50)),
                    GeometryFill = new SolidColorPaint(SKColor.Parse("#10B981")),
                    GeometryStroke = new SolidColorPaint(SKColor.Parse("#FFFFFF")) { StrokeThickness = 3 },
                    GeometrySize = 10
                }
            };

            AreaChartXAxes = new Axis[]
            {
                new Axis
                {
                    Labels = labels.ToArray(),
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(SKColor.Parse("#E2E8F0")),
                    SeparatorsAtCenter = false,
                    TicksPaint = new SolidColorPaint(SKColor.Parse("#E2E8F0")),
                    TicksAtCenter = true
                }
            };

            // Ẩn trục Y
            AreaChartYAxes = new Axis[] { new Axis { IsVisible = false } };
        }
        private void CreateLineChartData(IEnumerable<LoanTrendStats> stats)
        {
            var borrowValues = new List<int>();
            var returnValues = new List<int>();
            var labels = new List<string>();

            foreach (var item in stats)
            {
                borrowValues.Add(item.BorrowedCount);
                returnValues.Add(item.ReturnedCount);
                labels.Add($"T{item.Month}"); // Nhãn trục X: T7, T8...
            }

            LineChartSeries = new ISeries[]
            {
            // Đường 1: Sách mượn (Màu Xanh Dương)
            new LineSeries<int>
            {
                Name = "Sách mượn",
                Values = borrowValues.ToArray(),
                Stroke = new SolidColorPaint(SKColor.Parse("#2563EB")) { StrokeThickness = 3 }, // Màu xanh dương đậm
                Fill = null, // Không tô màu nền (chỉ vẽ đường)
                GeometrySize = 10, // Kích thước chấm tròn
                GeometryFill = new SolidColorPaint(SKColor.Parse("#2563EB")),
                GeometryStroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 2 },
                LineSmoothness = 0.5 // Độ cong mềm mại (0 = thẳng tuột, 1 = rất cong)
            },

            // Đường 2: Sách trả (Màu Xanh Lá)
            new LineSeries<int>
            {
                Name = "Sách trả",
                Values = returnValues.ToArray(),
                Stroke = new SolidColorPaint(SKColor.Parse("#10B981")) { StrokeThickness = 3 }, // Màu xanh lá
                Fill = null,
                GeometrySize = 10,
                GeometryFill = new SolidColorPaint(SKColor.Parse("#10B981")),
                GeometryStroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 2 },
                LineSmoothness = 0.5
            }
            };

            LineChartXAxes = new Axis[]
            {
            new Axis
            {
                Labels = labels.ToArray(),
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(SKColor.Parse("#E2E8F0")),
                TicksPaint = new SolidColorPaint(SKColor.Parse("#E2E8F0"))
            }
            };
        }
        private void CreateBarChartData(IEnumerable<CategoryLoanStats> stats)
        {
            var counts = new List<int>();
            var labels = new List<string>();

            foreach (var item in stats)
            {
                counts.Add(item.LoanCount);
                labels.Add(item.CategoryName);
            }

            BarChartSeries = new ISeries[]
            {
            new ColumnSeries<int>
                {
                    Name = "Số lượt mượn",
                    Values = counts.ToArray(),
                    Fill = new SolidColorPaint(SKColor.Parse("#2563EB")), // Màu xanh ChanelBackground
                    MaxBarWidth = 50, // Độ rộng tối đa của cột
                    Rx = 6, // Bo góc trên của cột (cho mềm mại)
                    Ry = 6
                }
            };

            BarChartXAxes = new Axis[]
            {
                new Axis
                {
                    LabelsRotation = 0, 
                    //MinStep = 1,
                    //ForceStepToMin = true,
                    Labels = labels.ToArray(),
                    SeparatorsPaint = null, // Ẩn lưới dọc
                    TextSize = 12
                }
            };

            // Trục Y: Ẩn hoặc hiện lưới ngang tùy ý
            BarChartYAxes = new Axis[]
            {
                new Axis
                {
                    SeparatorsPaint = new SolidColorPaint(SKColor.Parse("#E2E8F0")) { StrokeThickness = 1, PathEffect = new DashEffect(new float[] { 3, 3 }) }
                }
            };
        }
        private string CalculateTimeAgo(DateTime date)
        {
            var timeSpan = DateTime.Now - date;

            if (timeSpan.TotalMinutes < 1) return "Vừa xong";
            if (timeSpan.TotalMinutes < 60) return $"{(int)timeSpan.TotalMinutes} phút trước";
            if (timeSpan.TotalHours < 24) return $"{(int)timeSpan.TotalHours} giờ trước";
            if (timeSpan.TotalDays < 7) return $"{(int)timeSpan.TotalDays} ngày trước";

            return date.ToString("dd/MM/yyyy");
        }
        private async Task LoadRecentActivities()
        {
            // 1. Lấy dữ liệu thô
            var recentLoans = await _loanService.GetRecentLoansAsync(5); // Lấy 5 phiếu mượn mới nhất
            var recentStudents = await _studentService.GetRecentStudentsAsync(5); // Lấy 5 SV mới nhất

            var rawList = new List<(DateTime Date, string Type, string Name, string Detail)>();

            // 2. Chuyển đổi Loans thành dạng chung
            foreach (var loan in recentLoans)
            {
                rawList.Add((loan.LoanDate, "Borrow", loan.Student.StudentName, "mượn sách"));

                if (loan.LoanStatus == "1" && loan.ReturnDate is DateTime returnDate)
                {
                    rawList.Add((returnDate, "Return", loan.Student.StudentName, "trả sách"));
                }
            }

            // 3. Chuyển đổi Students thành dạng chung
            foreach (var student in recentStudents)
            {
                rawList.Add((student.RegistrationDate, "Register", student.StudentName, "đăng ký tài khoản"));
            }

            // 4. Sắp xếp lại tổng hợp và lấy 5 cái mới nhất
            var sortedActivities = rawList.OrderByDescending(x => x.Date).Take(NumberOfRecentActivities);

            // 5. Tạo ActivityDisplayItem cho View
            var displayList = new ObservableCollection<ActivityDisplayItem>();

            foreach (var item in sortedActivities)
            {
                var displayItem = new ActivityDisplayItem
                {
                    Description = $"{item.Name} {item.Detail}",
                    TimeAgo = CalculateTimeAgo(item.Date),
                };

                switch (item.Type)
                {
                    case "Borrow":
                        displayItem.Icon = "Book";
                        displayItem.IconColor = new SolidColorBrush(Color.FromRgb(14, 165, 233)); // #0EA5E9 (InUse)
                        displayItem.BackgroundColor = new SolidColorBrush(Color.FromRgb(224, 242, 249)); // #E0F2F9 (InUse_light)
                        break;

                    case "Return":
                        displayItem.Icon = "BookOpen";
                        displayItem.IconColor = new SolidColorBrush(Color.FromRgb(16, 185, 129)); // #10B981 (Success)
                        displayItem.BackgroundColor = new SolidColorBrush(Color.FromRgb(225, 244, 239)); // #E1F4EF (Success_light)
                        break;

                    case "Register":
                        displayItem.Icon = "UserPlus";
                        displayItem.IconColor = new SolidColorBrush(Color.FromRgb(37, 99, 235)); // #2563EB (Chanel)
                        displayItem.BackgroundColor = new SolidColorBrush(Color.FromRgb(238, 243, 255)); // #EEF3FF
                        break;
                }

                displayList.Add(displayItem);
            }

            RecentActivitiesList = displayList;
        }
    }

}
