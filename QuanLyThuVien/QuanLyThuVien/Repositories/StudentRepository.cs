using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;



namespace QuanLyThuVien.Repositories
{
    public class StudentRepository : IStudentService
    {
        private readonly DataContext _dataContext;

        private bool MatchSearchKeyword(Students s, string keyword)
        {
            return EF.Functions.Like(s.StudentName!, $"%{keyword}%") ||
                EF.Functions.Like(s.Email!, $"%{keyword}%") ||
                EF.Functions.Like(s.StudentId.ToString(), $"%{keyword}%") ||
                EF.Functions.Like(s.PhoneNumber!, $"%{keyword}%") ||
                EF.Functions.Like(s.Faculty.FacultyName!, $"%{keyword}%");
        }

        public StudentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Students>> GetAllStudentsAsync(string? keyword = null)
        {
            var q = _dataContext.Students
                .Include(s => s.Faculty) //  Để lấy thông tin khoa luôn
                .AsNoTracking(); // Đọc thôi nên là không cần track 

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                q = q.Where(s => MatchSearchKeyword(s, keyword));
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
            var existing = await _dataContext.Students.FindAsync(s.StudentId);
            if (existing != null)
            {
                _dataContext.Entry(existing).CurrentValues.SetValues(s);
                await _dataContext.SaveChangesAsync();
            }
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

        public async Task<IEnumerable<MonthlyReaderStats>> GetNewReadersStatsAsync()
        {
            var numOfMonths = 10; // Lấy 10 tháng
            var startDate = DateTime.Now.AddMonths(-numOfMonths + 1);
            startDate = new DateTime(startDate.Year, startDate.Month, 1);

            // Query Database (Giữ nguyên logic Group By của bạn)
            var dbData = await _dataContext.Students
                .Where(s => s.RegistrationDate >= startDate)
                .GroupBy(s => new { s.RegistrationDate.Year, s.RegistrationDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync();

            // Logic lấp đầy các tháng trống (vẫn cần thiết)
            var result = new List<MonthlyReaderStats>();

            for (int i = numOfMonths - 1; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var year = date.Year;
                var month = date.Month;

                var existing = dbData.FirstOrDefault(x => x.Year == year && x.Month == month);

                result.Add(new MonthlyReaderStats
                {
                    Month = month,
                    Year = year,
                    Count = existing?.Count ?? 0
                });
            }

            return result;
        }
        public async Task<IEnumerable<Students>> GetRecentStudentsAsync(int count)
        {
            return await _dataContext.Students
                .OrderByDescending(s => s.RegistrationDate)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Students>> GetStudentsPage(int offset, int size, string? keyword = null)
        {
            if (keyword is null)
                return await _dataContext.Students
                    .Include(st => st.Faculty)
                    .Skip(offset * size)
                    .Take(size)
                    .ToListAsync();
            return await _dataContext.Students
                .Include(st => st.Faculty)
                .Where(
                    s => EF.Functions.Like(s.StudentName!, $"%{keyword}%") ||
                         EF.Functions.Like(s.Email!, $"%{keyword}%") ||
                         EF.Functions.Like(s.StudentId.ToString(), $"%{keyword}%") ||
                         EF.Functions.Like(s.PhoneNumber!, $"%{keyword}%") ||
                         EF.Functions.Like(s.Faculty.FacultyName!, $"%{keyword}%"))
                .Skip(offset * size)
                .Take(size)
                .ToListAsync();
        }
        public async Task<int> GetTotalPages(int size, string? keyword = null)
        {
            IQueryable<Students> q;
            if (keyword is null)
                q = _dataContext.Students;
            else
                q = _dataContext.Students
                    .Where(
                        s => EF.Functions.Like(s.StudentName!, $"%{keyword}%") ||
                            EF.Functions.Like(s.Email!, $"%{keyword}%") ||
                            EF.Functions.Like(s.StudentId.ToString(), $"%{keyword}%") ||
                            EF.Functions.Like(s.PhoneNumber!, $"%{keyword}%") ||
                            EF.Functions.Like(s.Faculty.FacultyName!, $"%{keyword}%"));
            
            int remaining = await q.CountAsync() % size;
            int totalPages = await q.CountAsync() / size;
            if (remaining > 0)
            {
                totalPages++;
            }
            return totalPages;
        }

    }
}
