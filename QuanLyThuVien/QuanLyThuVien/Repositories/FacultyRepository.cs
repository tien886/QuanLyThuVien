using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Repositories
{
    public class FacultyRepository : IFacultyService
    {
        private readonly DataContext _dataContext;
        public FacultyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Faculties>> GetAllFacultiesAsync()
        {
            return await _dataContext.Faculties.ToListAsync();
        }
    }
}
