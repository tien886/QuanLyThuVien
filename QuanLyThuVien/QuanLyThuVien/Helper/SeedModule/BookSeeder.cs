using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class BookSeeder
    {
        public static void SeedBook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>().HasData(
                new Books
                {
                    BookID = 1,
                    ISBN = "9786047749001",
                    Title = "Lập Trình C# Cơ Bản",
                    Author = "Nguyễn Văn A",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2021,
                    Genre = "Programming",
                    CategoryID = 1
                },
                new Books
                {
                    BookID = 2,
                    ISBN = "9786047749002",
                    Title = "Thiết Kế Cơ Sở Dữ Liệu",
                    Author = "Trần Thị B",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2022,
                    Genre = "Database",
                    CategoryID = 2
                },
                new Books
                {
                    BookID = 3,
                    ISBN = "9786047749003",
                    Title = "Giải Thuật Nâng Cao",
                    Author = "Lê Văn C",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2020,
                    Genre = "Algorithm",
                    CategoryID = 1
                },
                new Books
                {
                    BookID = 4,
                    ISBN = "9786047749004",
                    Title = "Phân Tích Dữ Liệu với Python",
                    Author = "Phạm Thị D",
                    Publisher = "NXB Lao Động",
                    PublicationYear = 2023,
                    Genre = "Data Science",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 5,
                    ISBN = "9786047749005",
                    Title = "Trí Tuệ Nhân Tạo Cơ Bản",
                    Author = "Vũ Văn E",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2023,
                    Genre = "AI",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 6,
                    ISBN = "9786047749006",
                    Title = "Kỹ Thuật Lập Trình Java",
                    Author = "Đặng Thị F",
                    Publisher = "NXB Lao Động",
                    PublicationYear = 2022,
                    Genre = "Programming",
                    CategoryID = 1
                },
                new Books
                {
                    BookID = 7,
                    ISBN = "9786047749007",
                    Title = "Mạng Máy Tính Cơ Bản",
                    Author = "Ngô Văn G",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2021,
                    Genre = "Networking",
                    CategoryID = 2
                },
                new Books
                {
                    BookID = 8,
                    ISBN = "9786047749008",
                    Title = "Cấu Trúc Dữ Liệu và Giải Thuật",
                    Author = "Lý Thị H",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2020,
                    Genre = "Algorithm",
                    CategoryID = 1
                },
                new Books
                {
                    BookID = 9,
                    ISBN = "9786047749009",
                    Title = "Phát Triển Ứng Dụng Web",
                    Author = "Hoàng Văn I",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2023,
                    Genre = "Web Development",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 10,
                    ISBN = "9786047749010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 11,
                    ISBN = "9786047749010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 12,
                    ISBN = "9786047749010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 13,
                    ISBN = "9786047749010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 14,
                    ISBN = "9786047749010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 15,
                    ISBN = "9786047749010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 16,
                    ISBN = "9786047749010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 17,
                    ISBN = "9786047749010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 18,
                    ISBN = "9786047749010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3
                },
                new Books
                {
                    BookID = 19,
                    ISBN = "9786047749010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3
                }
            );
        }
    }
}
