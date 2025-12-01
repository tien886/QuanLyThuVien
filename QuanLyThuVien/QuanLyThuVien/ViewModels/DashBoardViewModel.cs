using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Services;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace QuanLyThuVien.ViewModels
{
    public partial class DashBoardViewModel : ObservableObject
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly IBookService _bookService;
        private readonly IStudentService _studentService;
        private readonly ILoanService _loanService; 
        public DashBoardViewModel(
            IBookCopyService bookCopyService,
            IBookService bookService,
            IStudentService studentService,
            ILoanService loanService
            )
        {
            _bookCopyService = bookCopyService;
            _bookService = bookService;
            _studentService = studentService;
            _loanService = loanService;
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

        // Hai thuộc tính cho LineChart xu hướng mượn sách 
        [ObservableProperty]
        private ISeries[] _lineChartSeries;

        [ObservableProperty]
        private Axis[] _lineChartXAxes;

        // Ba thuộc tính cho LineChart xu hướng mượn sách   
        [ObservableProperty]
        private ISeries[] _barChartSeries;

        [ObservableProperty]
        private Axis[] _barChartXAxes;

        [ObservableProperty]
        private Axis[] _barChartYAxes;

        // Thuộc tính chứa danh sách Top 5 Sách được mượn nhiều nhất
        [ObservableProperty]
        private IEnumerable<BookLoanStats> _topBooksList;

        // Thuộc tính chứa các hoạt động gần đây
        [ObservableProperty]
        private ObservableCollection<ActivityDisplayItem> _recentActivitiesList;


        public async Task LoadPage()
        {
            // Load các số liệu đơn
            TotalCopies = await _bookCopyService.GetTotalBookCopiesAsync();
            TotalBooks = await _bookService.GetTotalBooksAsync();

            // 1. Load và tạo biểu đồ Tròn
            var statusStats = await _bookCopyService.GetBookStatusStatsAsync();
            PieChartSeries = CreatePieChartData(statusStats);

            // 2. Load và tạo biểu đồ Vùng
            var readerStats = await _studentService.GetNewReadersStatsAsync();
            CreateAreaChartData(readerStats); // Hàm này sẽ gán vào AreaChartSeries và AreaChartXAxes

            // 3. Load và tạo biểu đồ Đường
            var trendsData = await _loanService.GetLoanTrendsAsync();
            CreateLineChartData(trendsData);

            // 4. Load và tạo biểu đồ Cột
            var categoryStats = await _loanService.GetLoanStatsByCategoryAsync();
            CreateBarChartData(categoryStats);

            // 5. Load Top sách mượn nhiều nhất
            TopBooksList = await _loanService.GetTopBorrowedBooksAsync();

            // 4. Load Hoạt động gần đây
            await LoadRecentActivities();
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
        private async Task LoadRecentActivities()
        {
            // 1. Lấy dữ liệu thô
            var recentLoans = await _loanService.GetRecentLoansAsync(5); // Lấy 5 phiếu mượn mới nhất
            var recentStudents = await _studentService.GetRecentStudentsAsync(5); // Lấy 5 SV mới nhất

            var rawList = new List<(DateTime Date, string Type, string Name, string Detail)>();

            // 2. Chuyển đổi Loans thành dạng chung
            foreach (var loan in recentLoans)
            {
                // Nếu LoanStatus là "0" (đang mượn) -> Mượn. Nếu "1" (đã trả) -> Trả
                // Ở đây tôi giả định logic đơn giản dựa vào ngày tạo phiếu
                rawList.Add((loan.LoanDate, "Borrow", loan.Student.StudentName, "mượn sách"));

                // Nếu muốn hiện cả hoạt động TRẢ sách, bạn cần check ReturnDate
                if (loan.ReturnDate != DateTime.MinValue && loan.LoanStatus == "1") // Giả sử 1 là đã trả
                {
                    rawList.Add((loan.ReturnDate, "Return", loan.Student.StudentName, "trả sách"));
                }
            }

            // 3. Chuyển đổi Students thành dạng chung
            foreach (var std in recentStudents)
            {
                rawList.Add((std.RegistrationDate, "Register", std.StudentName, "đăng ký tài khoản"));
            }

            // 4. Sắp xếp lại tổng hợp và lấy 5 cái mới nhất
            var sortedActivities = rawList.OrderByDescending(x => x.Date).Take(5);

            // 5. Tạo ActivityDisplayItem cho View
            var displayList = new ObservableCollection<ActivityDisplayItem>();

            foreach (var item in sortedActivities)
            {
                var displayItem = new ActivityDisplayItem
                {
                    Description = $"{item.Name} {item.Detail}",
                    TimeAgo = CalculateTimeAgo(item.Date),
                };

                // Gán Icon và Màu sắc dựa trên loại hoạt động
                switch (item.Type)
                {
                    case "Borrow": // Mượn (Màu Xanh Dương)
                        displayItem.Icon = "Book";
                        displayItem.IconColor = new SolidColorBrush(Color.FromRgb(14, 165, 233)); // #0EA5E9 (InUse)
                        displayItem.BackgroundColor = new SolidColorBrush(Color.FromRgb(224, 242, 249)); // #E0F2F9 (InUse_light)
                        break;

                    case "Return": // Trả (Màu Xanh Lá)
                        displayItem.Icon = "BookOpen";
                        displayItem.IconColor = new SolidColorBrush(Color.FromRgb(16, 185, 129)); // #10B981 (Success)
                        displayItem.BackgroundColor = new SolidColorBrush(Color.FromRgb(225, 244, 239)); // #E1F4EF (Success_light)
                        break;

                    case "Register": // Đăng ký (Màu Tím/Xanh đậm - Chanel)
                        displayItem.Icon = "UserPlus";
                        displayItem.IconColor = new SolidColorBrush(Color.FromRgb(37, 99, 235)); // #2563EB (Chanel)
                        displayItem.BackgroundColor = new SolidColorBrush(Color.FromRgb(238, 243, 255)); // #EEF3FF
                        break;
                }

                displayList.Add(displayItem);
            }

            RecentActivitiesList = displayList;
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
    }
}
