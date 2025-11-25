using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
