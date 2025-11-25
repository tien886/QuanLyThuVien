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
    public class LocationRepository : ILocationService
    {
        private readonly DataContext _dataContext;
        public LocationRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Locations>> GetAllLocationsAsync()
        {
            return await _dataContext.Locations.ToListAsync();
        }
    }
}
