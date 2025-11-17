using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
    public class Books
    {
        [Key]
        public int BookID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public int GenreID { get; set; }
        public int CategoryID { get; set; }
        public string Description { get; set; } 
        // Navigation property
        public BookCategories BookCategory { get; set; }
        public Genres Genre { get; set; }
        public ICollection<BookCopies> BookCopies { get; set; }
        // break db rule 
        public int TotalCopies => BookCopies?.Count ?? 0;
        public int AvailableCount => BookCopies?.Count(c => c.Status == "1") ?? 0;
    }
}
