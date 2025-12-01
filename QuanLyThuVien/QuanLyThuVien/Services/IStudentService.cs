using QuanLyThuVien.DTOs;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Students>> GetAllStudentsAsync(string? keyword = null);
        Task AddStudentAsync(Students s);
        Task UpdateStudentAsync(Students s);
        Task DeleteStudentAsync(int id);

        Task BlockStudentAsync(int id);  
        Task UnblockStudentAsync(int id);
        Task<IEnumerable<MonthlyReaderStats>> GetNewReadersStatsAsync();
        Task<IEnumerable<Students>> GetRecentStudentsAsync(int count);
    }
}
