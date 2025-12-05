using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genres>> GetAllGenresAsync();
    }
}
