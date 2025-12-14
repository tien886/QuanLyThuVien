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

        public async Task AddCategoryAsync(BookCategories newCategory)
        {
            _dataContext.BookCategories.Add(newCategory);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _dataContext.BookCategories.FindAsync(id);
            if (category != null)
            {
                _dataContext.BookCategories.Remove(category);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookCategories>> GetAllBookCategoriesAsync()
        {
            return await _dataContext.BookCategories.ToListAsync();
        }

        public async Task UpdateCategoryAsync(BookCategories category)
        {
            var existing = await _dataContext.Students.FindAsync(category.CategoryID);
            if (existing != null)
            {
                _dataContext.Entry(existing).CurrentValues.SetValues(category);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
