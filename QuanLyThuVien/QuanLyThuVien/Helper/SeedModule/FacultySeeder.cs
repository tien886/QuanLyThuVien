using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class FacultySeeder
    {
        public static void SeedFaculties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Faculties>().HasData(
                new Models.Faculties { FacultyID = 1, FacultyName = "Khoa Công nghệ phần mềm" },
                new Models.Faculties { FacultyID = 2, FacultyName = "Khoa Kỹ thuật máy tính" },
                new Models.Faculties { FacultyID = 3, FacultyName = "Khoa Khoa học máy tính" },
                new Models.Faculties { FacultyID = 4, FacultyName = "Khoa Mạng máy tính và truyền thông" },
                new Models.Faculties { FacultyID = 5, FacultyName = "Khoa Khoa học và kỹ thuật thông tin" },
                new Models.Faculties { FacultyID = 6, FacultyName = "Khoa Hệ thống thông tin" }
            );
        }
    }
}
