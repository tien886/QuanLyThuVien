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
        public async Task<IEnumerable<BookCopies>> GetAllBookCopiesAsync()
        {
            return _dataContext.BookCopies.ToList();
        }
        public async Task<int> GetTotalBookCopiesAsync()
        {
            return _dataContext.BookCopies.Count();
        }
    }
}
