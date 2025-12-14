using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IFacultyService
    {
        Task AddFacultyAsync(Faculties newItem);
        Task DeleteFacultyAsync(int id);
        Task<IEnumerable<Faculties>> GetAllFacultiesAsync();
        Task UpdateFacultyAsync(Faculties original);
    }
}
