using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class GenreSeeder
    {
        public static void SeedGenres(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genres>().HasData(
                new Genres
                {
                    GenreID = 1,
                    GenreName = "Sách giáo khoa",
                    LoanDurationDays = 60
                },
                new Genres
                {
                    GenreID = 2,
                    GenreName = "Sách tham khảo",
                    LoanDurationDays = 60
                },
                new Genres
                {
                    GenreID = 3,
                    GenreName = "Sách văn học - nghệ thuật",
                    LoanDurationDays = 30
                },
                new Genres
                {
                    GenreID = 4,
                    GenreName = "Báo tạp chí",
                    LoanDurationDays = 30
                },
                new Genres
                {
                    GenreID = 5,
                    GenreName = "Luận văn - Luận án - Khóa luận",
                    LoanDurationDays = 90
                },
                new Genres
                {
                    GenreID = 6,
                    GenreName = "Giáo trình nội bộ",
                    LoanDurationDays = 365
                }
            );
        }
    }
}
