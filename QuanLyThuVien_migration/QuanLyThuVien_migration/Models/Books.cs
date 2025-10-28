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
        public string title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationYear { get; set; }
        public string Genre { get; set; }
        public int CategoryID { get; set; }

        // Navigation property
        public BookCategories BookCategory { get; set; }
        public ICollection<BookCopies> BookCopies { get; set; }
    }
}
