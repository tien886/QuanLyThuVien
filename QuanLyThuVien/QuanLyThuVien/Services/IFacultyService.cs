using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IFacultyService
    {
        Task<IEnumerable<Faculties>> GetAllFacultiesAsync();
    }
}
