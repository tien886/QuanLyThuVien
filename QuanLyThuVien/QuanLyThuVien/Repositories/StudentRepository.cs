using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using SkiaSharp;
using System.Globalization;


namespace QuanLyThuVien.Repositories
{
    public class StudentRepository : IStudentService
    {
        private readonly DataContext _dataContext;
        public StudentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //public async Task<IEnumerable<Students>> GetAllStudentsAsync()
        //{
        //    return await _dataContext.Students
        //        .Include(s => s.Loans)
        //        .ToListAsync();
        //}
        public async Task<IEnumerable<Students>> GetAllStudentsAsync(string? keyword = null)
        {
            var q = _dataContext.Students.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                q = q.Where(s =>
                    EF.Functions.Like(s.StudentName!, $"%{keyword}%") ||
                    EF.Functions.Like(s.Email!, $"%{keyword}%") ||
                    EF.Functions.Like(s.StudentId.ToString(), $"%{keyword}%"));
            }

            return await q.OrderBy(s => s.StudentName).ToListAsync();
        }

        public async Task AddStudentAsync(Students s)
        {
            _dataContext.Students.Add(s);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Students s)
        {
            _dataContext.Students.Update(s);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var s = await _dataContext.Students.FindAsync(id);
            if (s is null)
                return;
            _dataContext.Students.Remove(s);
            await _dataContext.SaveChangesAsync();
        }

        public async Task BlockStudentAsync(int id)
        {
            var s = await _dataContext.Students.FindAsync(id);
            if (s is null)
                return;
            s.AccountStatus = "Vô hiệu hóa";
            await _dataContext.SaveChangesAsync();
        }

        public async Task UnblockStudentAsync(int id)
        {
            var s = await _dataContext.Students.FindAsync(id);
            if (s is null)
                return;
            s.AccountStatus = "Hoạt động";
            await _dataContext.SaveChangesAsync();
        }
        //public async Task ChangeStatus(Students students)
        //{
        //    var s = await _dataContext.Students.FindAsync(students.StudentId);
        //    if (s is null)
        //        return;
        //    students.AccountStatus = students.AccountStatus == "Active" ? "Disabled" : "Active";
        //    Debug.WriteLine($"Sinh viên {students.StudentName} hiện có trạng thái: {students.AccountStatus} trong repo");

        //    await _dataContext.SaveChangesAsync();
        //}

        public async Task<(ISeries[] Series, Axis[] XAxes)> GetNewReadersData()
        {
            var numOfMonths = 10;

            var numOfMonthsAgo = DateTime.Now.AddMonths(-numOfMonths).Date;
            var startDate = new DateTime(numOfMonthsAgo.Year, numOfMonthsAgo.Month, 1);

            var monthlyCounts = await _dataContext.Students
                .Where(s => s.RegistrationDate >= startDate) 
                .GroupBy(s => new { s.RegistrationDate.Year, s.RegistrationDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count() 
                })
                .ToListAsync(); 

            var labels = new List<string>();
            var counts = new List<int>();

            var culture = new CultureInfo("vi-VN");

            for (int i = numOfMonths - 1; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var year = date.Year;
                var month = date.Month;

                labels.Add($"T{month}/{year.ToString().Substring(2)}");

                var dataForMonth = monthlyCounts.FirstOrDefault(m => m.Year == year && m.Month == month);

                if (dataForMonth != null)
                {
                    counts.Add(dataForMonth.Count);
                }
                else
                {
                    counts.Add(0);
                }
            }
            var areaSeries = new ISeries[]
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

            var xAxes = new Axis[]
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

            return (areaSeries, xAxes);
        }
    }
}
