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
            return await _dataContext.BookCopies.CountAsync();
        }
        public async Task<int> AddBookCopiesAsync(BookCopies bookCopies)
        {
            await _dataContext.BookCopies.AddAsync(bookCopies);
            return await _dataContext.SaveChangesAsync();
        }
        public async Task<string> GetNextAvailableBookCopyID()
        {
            var maxIdString = await _dataContext.BookCopies
                                    .OrderByDescending(bc => bc.CopyID)
                                    .Select(bc => bc.CopyID)
                                    .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(maxIdString))
                return "C0001";

            var numericPart = maxIdString.Substring(1);
            if (!int.TryParse(numericPart, out int maxNumericId))
                maxNumericId = 0;
            string AvailableID  = "C" + (maxNumericId + 1).ToString("D4");
            return AvailableID;
        }
    }
}
