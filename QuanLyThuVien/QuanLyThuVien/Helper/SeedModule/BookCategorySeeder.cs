using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    LoanDurationDays = 14,
                    disciption = "Sách lập trình và kỹ thuật phần mềm"
                },
                new BookCategories
                {
                    CategoryID = 2,
                    CategoryName = "Database",
                    LoanDurationDays = 10,
                    disciption = "Sách về cơ sở dữ liệu và quản lý thông tin"
                },
                new BookCategories
                {
                    CategoryID = 3,
                    CategoryName = "AI & Data Science",
                    LoanDurationDays = 7,
                    disciption = "Sách về trí tuệ nhân tạo, học máy và khoa học dữ liệu"
                }
            );
        }
    }
}
