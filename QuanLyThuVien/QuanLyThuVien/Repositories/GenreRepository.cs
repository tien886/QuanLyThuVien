using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Repositories
{
    public class GenreRepository : IGenreService
    {
        private readonly DataContext _dataContext;
        public GenreRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }   
        public async Task<IEnumerable<Genres>> GetAllGenresAsync()
        {
            return await _dataContext.Genres.ToListAsync();
        }
        public async Task<Genres>GetGenreByID(int Id)
        {
            return await _dataContext.Genres.FindAsync(Id);
        }
    }
}
