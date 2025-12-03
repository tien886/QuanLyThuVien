using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class Locations
    {
        [Key]
        public int LocationID { get; set; }
        public string LocName { get; set; }
        // navigation property
        public ICollection<BookCopies> BookCopies { get; set; }
    }
}
