using LiveChartsCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopies>> GetAllBookCopiesAsync(Books book);
        Task<int> GetTotalBookCopiesAsync();
        Task<int> AddBookCopiesAsync(BookCopies bookCopies);
        Task<string> GetNextAvailableBookCopyID();
        Task<ISeries[]> GetBookStatusData();
    }
}
