

using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Repositories
{
    public class BookCategoryRepository : IBookCategoryService
    {
        private readonly DataContext _dataContext;
        public BookCategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<BookCategories>> GetAllBookCategoriesAsync()
        {
            return await _dataContext.BookCategories.ToListAsync();
        }
    }
}
