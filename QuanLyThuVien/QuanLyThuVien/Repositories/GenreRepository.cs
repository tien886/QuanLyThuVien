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

        public async Task AddGenreAsync(Genres newGenre)
        {
            _dataContext.Genres.Add(newGenre);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await _dataContext.Genres.FindAsync(id);
            if (genre != null)
            {
                _dataContext.Genres.Remove(genre);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Genres>> GetAllGenresAsync()
        {
            return await _dataContext.Genres.ToListAsync();
        }

        public async Task UpdateGenreAsync(Genres genre)
        {
            var existing = await _dataContext.Genres.FindAsync(genre.GenreID);
            if (existing != null)
            {
                _dataContext.Entry(existing).CurrentValues.SetValues(genre);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
