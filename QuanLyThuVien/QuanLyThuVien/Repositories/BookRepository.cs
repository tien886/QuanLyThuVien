using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Repositories
{
    public class BookRepository : IBookService
    {
        private readonly DataContext _dataContext;
        public BookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        // Thêm, xóa sửa 
        public async Task<IEnumerable<Books>> GetAllBooksAsync()
        {
            return await _dataContext.Books
                .Include(b => b.BookCategory)
                .Include(b => b.BookCopies)
                .ToListAsync();
        }
        public async Task AddBookAsync(Books book)
        {
            _dataContext.Books.Add(book);
            await _dataContext.SaveChangesAsync();
        }
        public async Task UpdateBookAsync(Books book)
        {
            _dataContext.Books.Update(book);
            await _dataContext.SaveChangesAsync();
        }
        public async Task DeleteBookAsync(Books book)
        {
            _dataContext.Books.Remove(book);
            await _dataContext.SaveChangesAsync();
        }


        // Lấy data cho badge (DashBoard)
        public async Task<int> GetTotalBooksAsync()
        {
            return await _dataContext.Books.CountAsync();
        }


        // Phân trang 
        public async Task<int> GetTotalPages(int pageSize, string keyword = "")
        {
            var query = _dataContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(b => b.Title.Contains(keyword) ||
                                         b.Author.Contains(keyword) ||
                                         b.ISBN.Contains(keyword));
            }

            int totalCount = await query.CountAsync();
            return (int)Math.Ceiling((double)totalCount / pageSize);
        }
        public async Task<IEnumerable<Books>> GetBooksPage(int pageIndex, int pageSize, string keyword = "")
        {
            var query = _dataContext.Books
                .Include(b => b.BookCategory)
                .Include(b => b.Genre)
                .Include(b => b.BookCopies) // Quan trọng để đếm số lượng
                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(b => b.Title.Contains(keyword) ||
                                         b.Author.Contains(keyword) ||
                                         b.ISBN.Contains(keyword));
            }

            return await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
