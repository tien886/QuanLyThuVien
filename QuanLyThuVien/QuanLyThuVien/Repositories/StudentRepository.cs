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

        public StudentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Thêm, xóa, sửa, ban/unban
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


        // Lấy data cho sinh viên đăng kí trong các tháng (Dashboard)
        public async Task<IEnumerable<MonthlyReaderStats>> GetNewReadersStatsAsync(DateTime startDate, DateTime endDate)
        {
            // Đảm bảo startDate là ngày đầu tháng
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            // Đảm bảo endDate là ngày đầu tháng kế tiếp (để dễ so sánh < endDate)
            endDate = new DateTime(endDate.Year, endDate.Month, 1).AddMonths(1);

            // Lấy dữ liệu từ DB trong khoảng thời gian
            var dbData = await _dataContext.Students
                .Where(s => s.RegistrationDate >= startDate && s.RegistrationDate < endDate)
                .GroupBy(s => new { s.RegistrationDate.Year, s.RegistrationDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync();

            // Logic lấp đầy các tháng trống
            var result = new List<MonthlyReaderStats>();

            var current = startDate;
            while (current < endDate)
            {
                var year = current.Year;
                var month = current.Month;

                var existing = dbData.FirstOrDefault(x => x.Year == year && x.Month == month);

                result.Add(new MonthlyReaderStats
                {
                    Month = month,
                    Year = year,
                    Count = existing?.Count ?? 0
                });

                current = current.AddMonths(1);
            }

            return result;
        }


        // Lấy data cho hoạt động gần đây (Dashboard)
        public async Task<IEnumerable<Students>> GetRecentStudentsAsync(int count)
        {
            return await _dataContext.Students
                .OrderByDescending(s => s.RegistrationDate)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }


        // Phân trang 
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


        // Lấy sinh viên (Thêm phiếu mượn)
        public async Task<Students> GetStudentByIDAsync(int id)
        {
            return await _dataContext.Students.FindAsync(id);
        }
    }
}
