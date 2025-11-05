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
                    disciption = "Sách lập trình và kỹ thuật phần mềm."
                },
                new BookCategories
                {
                    CategoryID = 2,
                    CategoryName = "Database",
                    LoanDurationDays = 10,
                    disciption = "Sách về cơ sở dữ liệu và quản lý thông tin."
                },
                new BookCategories
                {
                    CategoryID = 3,
                    CategoryName = "AI & Data Science",
                    LoanDurationDays = 7,
                    disciption = "Sách về trí tuệ nhân tạo, học máy và khoa học dữ liệu."
                },
                new BookCategories
                {
                    CategoryID = 4,
                    CategoryName = "Networking",
                    LoanDurationDays = 10,
                    disciption = "Sách về mạng máy tính, giao thức và hạ tầng mạng."
                },
                new BookCategories
                {
                    CategoryID = 5,
                    CategoryName = "Cybersecurity",
                    LoanDurationDays = 7,
                    disciption = "Sách về bảo mật thông tin, mã hóa và an ninh mạng."
                },
                new BookCategories
                {
                    CategoryID = 6,
                    CategoryName = "Web Development",
                    LoanDurationDays = 14,
                    disciption = "Sách về phát triển ứng dụng web frontend và backend."
                },
                new BookCategories
                {
                    CategoryID = 7,
                    CategoryName = "Software Engineering",
                    LoanDurationDays = 14,
                    disciption = "Sách về quy trình phát triển, thiết kế và kiểm thử phần mềm."
                },
                new BookCategories
                {
                    CategoryID = 8,
                    CategoryName = "Operating Systems",
                    LoanDurationDays = 10,
                    disciption = "Sách về hệ điều hành, quản lý tiến trình và bộ nhớ."
                },
                new BookCategories
                {
                    CategoryID = 9,
                    CategoryName = "Mobile Development",
                    LoanDurationDays = 14,
                    disciption = "Sách phát triển ứng dụng di động trên Android, iOS."
                },
                new BookCategories
                {
                    CategoryID = 10,
                    CategoryName = "Cloud Computing",
                    LoanDurationDays = 10,
                    disciption = "Sách về điện toán đám mây, dịch vụ và kiến trúc cloud."
                },
                new BookCategories
                {
                    CategoryID = 11,
                    CategoryName = "DevOps",
                    LoanDurationDays = 7,
                    disciption = "Sách về tự động hóa, CI/CD và quản lý hạ tầng."
                },
                new BookCategories
                {
                    CategoryID = 12,
                    CategoryName = "Game Development",
                    LoanDurationDays = 14,
                    disciption = "Sách về phát triển trò chơi, engine game và đồ họa thời gian thực."
                },
                new BookCategories
                {
                    CategoryID = 13,
                    CategoryName = "Internet of Things",
                    LoanDurationDays = 10,
                    disciption = "Sách về IoT, cảm biến và hệ thống nhúng kết nối mạng."
                },
                new BookCategories
                {
                    CategoryID = 14,
                    CategoryName = "Computer Graphics",
                    LoanDurationDays = 10,
                    disciption = "Sách về đồ họa máy tính, dựng hình và xử lý hình ảnh."
                },
                new BookCategories
                {
                    CategoryID = 15,
                    CategoryName = "Theoretical Computer Science",
                    LoanDurationDays = 14,
                    disciption = "Sách về lý thuyết tính toán, độ phức tạp và giải thuật."
                }
            );
        }
    }
}
