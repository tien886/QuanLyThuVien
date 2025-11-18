using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
