using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IBookCategoryService
    {
        Task<IEnumerable<BookCategories>> GetAllBookCategoriesAsync();
        Task<BookCategories> GetBookCategoryByID(int Id);
    }
}
