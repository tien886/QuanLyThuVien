using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
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
        //Task ChangeStatus(Students students);
        Task<(ISeries[] Series, Axis[] XAxes)> GetNewReadersData();
    }
}
