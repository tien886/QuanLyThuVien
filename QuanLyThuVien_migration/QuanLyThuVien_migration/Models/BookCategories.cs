

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class BookCategories
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int LoanDurationDays { get; set; }
        public string disciption { get; set; }
        // Navigation property
        public ICollection<Books> Books { get; set; }
    }
}
