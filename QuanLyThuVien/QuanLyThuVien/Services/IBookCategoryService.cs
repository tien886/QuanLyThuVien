using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IBookCategoryService
    {
        Task AddCategoryAsync(BookCategories newItem);
        Task DeleteCategoryAsync(int id);
        Task<IEnumerable<BookCategories>> GetAllBookCategoriesAsync();
        Task UpdateCategoryAsync(BookCategories original);
    }
}
