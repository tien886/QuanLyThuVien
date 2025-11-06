using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Students>> GetAllStudentsAsync(string? keyword = null);
        Task AddAsync(Students s);
        Task UpdateAsync(Students s);
        Task DeleteAsync(int id);

        Task BlockAsync(int id);  
        Task UnblockAsync(int id);
        Task ChangeStatus(Students students);
    }
}
