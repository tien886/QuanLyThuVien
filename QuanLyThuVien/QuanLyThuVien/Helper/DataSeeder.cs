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
            BookSeeder.SeedBooks(modelBuilder);
            Debug.WriteLine("Seed books successfullly");
            BookCategorySeeder.SeedBookCategory(modelBuilder);
            Debug.WriteLine("Seed bookcategory successfullly");
            GenreSeeder.SeedGenres(modelBuilder);
            Debug.WriteLine("Seed genre successfullly");
            BookCopySeeder.SeedBookCopies(modelBuilder);
            Debug.WriteLine("Seed bookcopies successfullly");
            StudentSeeder.SeedStudents(modelBuilder);
            Debug.WriteLine("Seed students successfullly");
            LocationSeeder.SeedLocations(modelBuilder);
            Debug.WriteLine("Seed locations successfully");
            FacultySeeder.SeedFaculties(modelBuilder);
            Debug.WriteLine("Seed faculties successfully");
            LoanSeeder.SeedLoans(modelBuilder);
            Debug.WriteLine("Seed loans successfully");
        }
    }
}
