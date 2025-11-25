using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genres>> GetAllGenresAsync();
    }
}
