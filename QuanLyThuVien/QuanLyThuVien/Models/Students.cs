using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    // Lớp POCO "ngốc" - không kế thừa gì cả
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountStatus { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Navigation pane (giữ nguyên)
        public ICollection<Loans> Loans { get; set; }

        public Students()
        {
            Loans = new HashSet<Loans>();
        }
    }
}