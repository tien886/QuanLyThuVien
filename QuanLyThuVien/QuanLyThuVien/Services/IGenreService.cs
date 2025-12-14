using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IGenreService
    {
        Task AddGenreAsync(Genres newGenre);
        Task DeleteGenreAsync(int id);
        Task<IEnumerable<Genres>> GetAllGenresAsync();
        Task UpdateGenreAsync(Genres originalGenre);
    }
}
