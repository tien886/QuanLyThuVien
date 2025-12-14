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

        public async Task AddFacultyAsync(Faculties newFacylty)
        {
            _dataContext.Faculties.Add(newFacylty);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteFacultyAsync(int id)
        {
            var faculty = await _dataContext.Faculties.FindAsync(id);
            if (faculty != null)
            {
                _dataContext.Faculties.Remove(faculty);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Faculties>> GetAllFacultiesAsync()
        {
            return await _dataContext.Faculties.ToListAsync();
        }

        public async Task UpdateFacultyAsync(Faculties faculty)
        {
            var existing = await _dataContext.Faculties.FindAsync(faculty.FacultyID);
            if (existing != null)
            {
                _dataContext.Entry(existing).CurrentValues.SetValues(faculty);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
