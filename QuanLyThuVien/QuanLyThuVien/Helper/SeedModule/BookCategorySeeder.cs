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
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 2,
                    CategoryName = "Database",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 3,
                    CategoryName = "AI & Data Science",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 4,
                    CategoryName = "Networking",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 5,
                    CategoryName = "Cybersecurity",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 6,
                    CategoryName = "Web Development",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 7,
                    CategoryName = "Software Engineering",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 8,
                    CategoryName = "Operating Systems",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 9,
                    CategoryName = "Mobile Development",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 10,
                    CategoryName = "Cloud Computing",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 11,
                    CategoryName = "DevOps",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 12,
                    CategoryName = "Game Development",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 13,
                    CategoryName = "Internet of Things",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 14,
                    CategoryName = "Computer Graphics",
                    LoanDurationDays = 30,
                },
                new BookCategories
                {
                    CategoryID = 15,
                    CategoryName = "Theoretical Computer Science",
                    LoanDurationDays = 30,
                }
            );
        }
    }
}
