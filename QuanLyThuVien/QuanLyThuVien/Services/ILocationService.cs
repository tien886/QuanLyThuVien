using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<Locations>> GetAllLocationsAsync();
    }
}
