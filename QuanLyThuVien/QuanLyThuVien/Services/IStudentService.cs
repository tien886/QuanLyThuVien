using QuanLyThuVien.DTOs;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Students>> GetAllStudentsAsync(string? keyword = null);
        Task AddStudentAsync(Students s);
        Task UpdateStudentAsync(Students s);
        Task<Students> GetStudentByIDAsync(int id);
        Task DeleteStudentAsync(int id);
        Task BlockStudentAsync(int id);  
        Task UnblockStudentAsync(int id);
        Task<IEnumerable<MonthlyReaderStats>> GetNewReadersStatsAsync();
        Task<IEnumerable<Students>> GetRecentStudentsAsync(int count);
        Task<IEnumerable<Students>> GetStudentsPage(int offsetm, int size, string? keyword);
        Task<int> GetTotalPages(int size, string? keyword = null);
    }
}
