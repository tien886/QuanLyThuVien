using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopies>> GetAllBookCopiesAsync(Books book);
        Task<int> GetTotalBookCopiesAsync();
        Task<string> GetNextAvailableBookCopyID();
    }
}
