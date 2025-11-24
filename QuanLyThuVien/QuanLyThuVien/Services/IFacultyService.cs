using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Services
{
    public interface IFacultyService
    {
        Task<IEnumerable<Faculties>> GetAllFacultiesAsync();
    }
}
