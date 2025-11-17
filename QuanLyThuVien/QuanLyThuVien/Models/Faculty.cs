using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class Faculties
    {
        [Key]
        public int FacultyID { get; set; }
        public string FacultyName { get; set; }
        // Navigation property
        public ICollection<Students> Students { get; set; }

    }
}
