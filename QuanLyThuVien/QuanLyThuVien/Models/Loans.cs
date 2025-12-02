
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class Loans
    {
        [Key]
        public int LoanID { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string LoanStatus { get; set; }
        // FK
        public string CopyID { get; set; }
        public int StudentID { get; set; }
        // Navigation property
        public BookCopies BookCopy { get; set; }
        public Students Student { get; set; }
        // 
        public string LoanStatusDescription
        {
            get
            {
                return LoanStatus switch
                {
                    "0" => "borrowed",
                    "-1" => "overdue"
                };
            }
        }
    }
}
