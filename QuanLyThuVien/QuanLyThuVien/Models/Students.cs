

using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountStatus { get; set; }
        public DateTime RegistrationDate { get; set; }    
        // Navigation pane 
        public ICollection<Loans> Loans { get; set; }
    }
}
