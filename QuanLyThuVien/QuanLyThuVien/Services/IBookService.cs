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
        //Task<IEnumerable<Books>> GetBooksPage(int offsetm, int size);
        //Task<int> GetTotalPages(int size);

        Task<int> GetTotalPages(int pageSize, string keyword = "");
        Task<IEnumerable<Books>> GetBooksPage(int pageIndex, int pageSize, string keyword = "");
    }

}
