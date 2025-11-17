
using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class BookCopies
    {
        [Key]
        public string CopyID { get; set; }
        public int BookID { get; set; }
        public string Status { get; set; } // e.g., 1, 0, -1
        public string Location { get; set; }
        public DateTime DateAdded { get; set; }

        // Navigation property
        public Books Book { get; set; }
        public ICollection<Loans> Loans { get; set; }
        // 
        public string StatusDescription
        {
            get
            {
                return Status switch
                {
                    "1" => "Có sẵn",
                    "0" => "Đang mượn",
                    "-1" => "Mất",
                    "-2" => "Hỏng"
                };
            }
        }
    }
}
