using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopies>> GetAllBookCopiesAsync();
        Task<int> GetTotalBookCopiesAsync();
    }
}
