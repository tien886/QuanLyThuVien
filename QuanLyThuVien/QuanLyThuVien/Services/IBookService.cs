
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IBookService 
    {
        
        Task<IEnumerable<Books>> GetAllBooksAsync();
        Task AddBookAsync(Books book);
        Task UpdateBookAsync(Books book);
        Task DeleteBookAsync (Books book);
        Task<int> GetTotalBooksAsync();
    }
}
