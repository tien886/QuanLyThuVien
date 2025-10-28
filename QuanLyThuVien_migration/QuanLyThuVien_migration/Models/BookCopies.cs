
using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class BookCopies
    {
        [Key]
        public string CopyID { get; set; }
        public int BookID { get; set; }
        public string Status { get; set; } // e.g., Available, Borrowed, Reserved
        public string Location { get; set; }
        public DateTime DateAdded { get; set; }

        // Navigation property
        public Books Book { get; set; }
    }
}
