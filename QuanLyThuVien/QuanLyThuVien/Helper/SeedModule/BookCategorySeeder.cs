using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class BookCategorySeeder
    {
        public static void SeedBookCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategories>().HasData(
                new BookCategories
                {
                    CategoryID = 1,
                    CategoryName = "Programming",
                },
                new BookCategories
                {
                    CategoryID = 2,
                    CategoryName = "Database",
                },
                new BookCategories
                {
                    CategoryID = 3,
                    CategoryName = "AI & Data Science",
                },
                new BookCategories
                {
                    CategoryID = 4,
                    CategoryName = "Networking",
                },
                new BookCategories
                {
                    CategoryID = 5,
                    CategoryName = "Cybersecurity",
                },
                new BookCategories
                {
                    CategoryID = 6,
                    CategoryName = "Web Development",
                },
                new BookCategories
                {
                    CategoryID = 7,
                    CategoryName = "Software Engineering",
                },
                new BookCategories
                {
                    CategoryID = 8,
                    CategoryName = "Operating Systems",
                },
                new BookCategories
                {
                    CategoryID = 9,
                    CategoryName = "Mobile Development",
                },
                new BookCategories
                {
                    CategoryID = 10,
                    CategoryName = "Cloud Computing",
                },
                new BookCategories
                {
                    CategoryID = 11,
                    CategoryName = "DevOps",
                },
                new BookCategories
                {
                    CategoryID = 12,
                    CategoryName = "Game Development",
                },
                new BookCategories
                {
                    CategoryID = 13,
                    CategoryName = "Internet of Things",
                },
                new BookCategories
                {
                    CategoryID = 14,
                    CategoryName = "Computer Graphics",
                },
                new BookCategories
                {
                    CategoryID = 15,
                    CategoryName = "Theoretical Computer Science",
                }
            );
        }
    }
}
