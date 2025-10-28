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
    public class BookRepository : IBookService
    {
        private readonly DataContext _dataContext;
        public BookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
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
        public async Task<int> GetTotalBooksAsync()
        {
            return await _dataContext.Books.CountAsync();
        }
    }
}
