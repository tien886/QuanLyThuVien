using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Helper.SeedModule;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Helper
{
    public class DataSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            BookSeeder.SeedBook(modelBuilder);
            Debug.WriteLine("Seed books succesfullly");
            BookCategorySeeder.SeedBookCategory(modelBuilder);
            Debug.WriteLine("Seed bookcategory succesfullly");
            BookCopySeeder.SeedBookCopy(modelBuilder);
            Debug.WriteLine("Seed bookcopies succesfullly");
            StudentSeeder.SeedStudent(modelBuilder);
            Debug.WriteLine("Seed students succesfullly");
        }
    }
}
