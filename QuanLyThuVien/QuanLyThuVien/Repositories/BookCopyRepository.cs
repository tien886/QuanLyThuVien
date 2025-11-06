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
    public class BookCopyRepository : IBookCopyService
    {
        private readonly DataContext _dataContext;
        public BookCopyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<BookCopies>> GetAllBookCopiesAsync(Books book)
        {
            return await _dataContext.BookCopies.
                Where(bc => bc.BookID == book.BookID && (bc.Status == "1" || bc.Status == "0")).ToListAsync();
        }
        public async Task<int> GetTotalBookCopiesAsync()
        {
            return _dataContext.BookCopies.Count();
        }
    }
}
