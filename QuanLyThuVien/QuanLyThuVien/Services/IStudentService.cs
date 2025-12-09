using QuanLyThuVien.DTOs;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IStudentService
    {
        // Thêm, xóa, sửa, ban/unban
        //Task<IEnumerable<Students>> GetAllStudentsAsync(string? keyword = null);
        Task AddStudentAsync(Students s);
        Task UpdateStudentAsync(Students s);
        Task DeleteStudentAsync(int id);
        Task BlockStudentAsync(int id);
        Task UnblockStudentAsync(int id);


        // Lấy data cho sinh viên đăng kí trong các tháng (Dashboard)
        Task<IEnumerable<MonthlyReaderStats>> GetNewReadersStatsAsync(DateTime startDate, DateTime endDate);


        // Lây data cho hoạt động gần đây (Dashboard)
        Task<IEnumerable<Students>> GetRecentStudentsAsync(int count);


        // Phân trang
        Task<IEnumerable<Students>> GetStudentsPage(int offsetm, int size, string? keyword);
        Task<int> GetTotalPages(int size, string? keyword = null);


        // Lấy sinh viên (Thêm phiếu mượn)
        Task<Students> GetStudentByIDAsync(int id);
    }
}
