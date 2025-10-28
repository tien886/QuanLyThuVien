using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class BookCopySeeder
    {
        public static void SeedBookCopy(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCopies>().HasData(
                new BookCopies
                {
                    CopyID = "C001",
                    BookID = 1,
                    Status = "Borrowed",
                    Location = "Shelf I1",
                    DateAdded = new DateTime(2023, 1, 2)
                },
                new BookCopies
                {
                    CopyID = "C002",
                    BookID = 1,
                    Status = "Reserved",
                    Location = "Shelf E1",
                    DateAdded = new DateTime(2023, 6, 22)
                },
                new BookCopies
                {
                    CopyID = "C003",
                    BookID = 1,
                    Status = "Reserved",
                    Location = "Shelf D2",
                    DateAdded = new DateTime(2023, 10, 24)
                },
                new BookCopies
                {
                    CopyID = "C004",
                    BookID = 2,
                    Status = "Borrowed",
                    Location = "Shelf G2",
                    DateAdded = new DateTime(2023, 4, 9)
                },
                new BookCopies
                {
                    CopyID = "C005",
                    BookID = 2,
                    Status = "Borrowed",
                    Location = "Shelf J1",
                    DateAdded = new DateTime(2023, 2, 14)
                },
                new BookCopies
                {
                    CopyID = "C006",
                    BookID = 2,
                    Status = "Available",
                    Location = "Shelf D1",
                    DateAdded = new DateTime(2023, 5, 16)
                },
                new BookCopies
                {
                    CopyID = "C007",
                    BookID = 3,
                    Status = "Available",
                    Location = "Shelf F1",
                    DateAdded = new DateTime(2023, 7, 8)
                },
                new BookCopies
                {
                    CopyID = "C008",
                    BookID = 3,
                    Status = "Reserved",
                    Location = "Shelf G2",
                    DateAdded = new DateTime(2023, 12, 19)
                },
                new BookCopies
                {
                    CopyID = "C009",
                    BookID = 3,
                    Status = "Borrowed",
                    Location = "Shelf C1",
                    DateAdded = new DateTime(2023, 4, 1)
                },
                new BookCopies
                {
                    CopyID = "C010",
                    BookID = 4,
                    Status = "Reserved",
                    Location = "Shelf A2",
                    DateAdded = new DateTime(2023, 11, 4)
                },
                new BookCopies
                {
                    CopyID = "C011",
                    BookID = 4,
                    Status = "Reserved",
                    Location = "Shelf J1",
                    DateAdded = new DateTime(2023, 1, 21)
                },
                new BookCopies
                {
                    CopyID = "C012",
                    BookID = 4,
                    Status = "Borrowed",
                    Location = "Shelf A2",
                    DateAdded = new DateTime(2023, 5, 27)
                },
                new BookCopies
                {
                    CopyID = "C013",
                    BookID = 5,
                    Status = "Available",
                    Location = "Shelf C1",
                    DateAdded = new DateTime(2023, 1, 17)
                },
                new BookCopies
                {
                    CopyID = "C014",
                    BookID = 5,
                    Status = "Reserved",
                    Location = "Shelf A2",
                    DateAdded = new DateTime(2023, 5, 4)
                },
                new BookCopies
                {
                    CopyID = "C015",
                    BookID = 5,
                    Status = "Available",
                    Location = "Shelf J1",
                    DateAdded = new DateTime(2023, 4, 19)
                },
                new BookCopies
                {
                    CopyID = "C016",
                    BookID = 6,
                    Status = "Borrowed",
                    Location = "Shelf D1",
                    DateAdded = new DateTime(2023, 10, 7)
                },
                new BookCopies
                {
                    CopyID = "C017",
                    BookID = 6,
                    Status = "Reserved",
                    Location = "Shelf J1",
                    DateAdded = new DateTime(2023, 10, 26)
                },
                new BookCopies
                {
                    CopyID = "C018",
                    BookID = 6,
                    Status = "Reserved",
                    Location = "Shelf F1",
                    DateAdded = new DateTime(2023, 2, 6)
                },
                new BookCopies
                {
                    CopyID = "C019",
                    BookID = 7,
                    Status = "Available",
                    Location = "Shelf D1",
                    DateAdded = new DateTime(2023, 9, 22)
                },
                new BookCopies
                {
                    CopyID = "C020",
                    BookID = 7,
                    Status = "Reserved",
                    Location = "Shelf C2",
                    DateAdded = new DateTime(2023, 1, 3)
                },
                new BookCopies
                {
                    CopyID = "C021",
                    BookID = 7,
                    Status = "Available",
                    Location = "Shelf A2",
                    DateAdded = new DateTime(2023, 12, 25)
                },
                new BookCopies
                {
                    CopyID = "C022",
                    BookID = 8,
                    Status = "Borrowed",
                    Location = "Shelf H1",
                    DateAdded = new DateTime(2023, 10, 28)
                },
                new BookCopies
                {
                    CopyID = "C023",
                    BookID = 8,
                    Status = "Available",
                    Location = "Shelf J1",
                    DateAdded = new DateTime(2023, 12, 2)
                },
                new BookCopies
                {
                    CopyID = "C024",
                    BookID = 8,
                    Status = "Available",
                    Location = "Shelf A2",
                    DateAdded = new DateTime(2023, 9, 8)
                },
                new BookCopies
                {
                    CopyID = "C025",
                    BookID = 9,
                    Status = "Available",
                    Location = "Shelf I1",
                    DateAdded = new DateTime(2023, 9, 17)
                },
                new BookCopies
                {
                    CopyID = "C026",
                    BookID = 9,
                    Status = "Borrowed",
                    Location = "Shelf H2",
                    DateAdded = new DateTime(2023, 8, 28)
                },
                new BookCopies
                {
                    CopyID = "C027",
                    BookID = 9,
                    Status = "Available",
                    Location = "Shelf H1",
                    DateAdded = new DateTime(2023, 1, 25)
                },
                new BookCopies
                {
                    CopyID = "C028",
                    BookID = 10,
                    Status = "Available",
                    Location = "Shelf H1",
                    DateAdded = new DateTime(2023, 4, 17)
                },
                new BookCopies
                {
                    CopyID = "C029",
                    BookID = 10,
                    Status = "Available",
                    Location = "Shelf B2",
                    DateAdded = new DateTime(2023, 6, 12)
                },
                new BookCopies
                {
                    CopyID = "C030",
                    BookID = 10,
                    Status = "Reserved",
                    Location = "Shelf H1",
                    DateAdded = new DateTime(2023, 1, 17)
                }
            );
        }
    }
}
