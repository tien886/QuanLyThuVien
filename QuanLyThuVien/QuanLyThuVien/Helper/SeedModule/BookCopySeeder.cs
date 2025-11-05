using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class BookCopySeeder
    {
        private static int startIndex = 1;
        private static int TotalCopies = 90000;
        private static int TotalBooks = 60;
        private static string MaximumDigitsOfCopyID = "D5";
        public static void EnsureAtleastOneCopiesPerBook(ModelBuilder modelBuilder)
        {
            int TotalBooks = 60;
            for(int bookId = 1; bookId <= TotalBooks; bookId++)
            {
                modelBuilder.Entity<BookCopies>().HasData(
                    new BookCopies
                    {
                        CopyID = $"C{startIndex.ToString(MaximumDigitsOfCopyID)}", // e.g., C0001, C0002, ...
                        BookID = bookId,
                        Status = "1",
                        Location = "Shelf A1",
                        DateAdded = DateTime.Now.AddDays(-30)
                    }
                    );
                startIndex++;
            }
        }
        public static void SeedBookCopy(ModelBuilder modelBuilder)
        {
            EnsureAtleastOneCopiesPerBook(modelBuilder);
            
            var random = new Random();
            string[] statuses = { "1", "0", "-1", "-2" }; // 1=Available, 0=Borrowed, -1=Lost, -2 = Damaged
            for(int i = 0; i < TotalCopies; i++)
            {
                var shelfLetter = (char)('A' + random.Next(0, 10)); // Shelves A–J
                var shelfNumber = random.Next(1, 20);               // Positions 1–19
                var status = statuses[random.Next(statuses.Length)];
                var dateAdded = DateTime.Now.AddDays(-random.Next(0, 365));
                modelBuilder.Entity<BookCopies>().HasData(
                    new BookCopies
                    {
                        CopyID = $"C{(startIndex + i).ToString("D4")}", // e.g., C0001, C0002, ...
                        BookID = random.Next(1, TotalBooks),
                        Status = status,
                        Location = $"Shelf {shelfLetter}{shelfNumber}",
                        DateAdded = dateAdded
                    }
                    );
            }    
        }
    }
}
