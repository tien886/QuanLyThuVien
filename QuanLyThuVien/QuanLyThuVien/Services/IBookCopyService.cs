using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopies>> GetAllBookCopiesAsync(Books book);
        Task<int> GetTotalBookCopiesAsync();
        Task<int> AddBookCopiesAsync(BookCopies bookCopies);
        Task<string> GetNextAvailableBookCopyID();
        Task<BookCopies> GetBookCopiesByIDAsync(string id);
        //Task<ISeries[]> GetBookStatusData();
        Task<BookStatusStats> GetBookStatusStatsAsync();
        Task<Genres> GetGenreByCopyIdAsync(string Id);
        Task<BookCategories> GetBookCategoryByCopyIDAsync(string Id);
        Task<Books> GetBookByCopyIdAsync(string copyId);
        Task<Locations> GetLocationByBookCopyID(string Id);
        Task UpdateCopiesAsync(BookCopies copies);
        Task DeleteCopiesAsync(BookCopies copies);
    }
}

    

