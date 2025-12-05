
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class Genres
    {
        [Key]
        public int GenreID { get; set; }
        public string GenreName { get; set; }
        public int LoanDurationDays { get; set; }

        // Navigation property
        public ICollection<Books> Books { get; set; }
    }
}
