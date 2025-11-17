using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Helper;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<BookCategories> BookCategories { get; set; }
        public DbSet<BookCopies> BookCopies { get; set; }
        public DbSet<Students> Students { get; set; }   
        public DbSet<Loans> Loans { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
        public DbSet<Genres> Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // one category to many books
            modelBuilder.Entity<Books>()
                .HasOne(b => b.BookCategory)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);
            // one genre to many books
            modelBuilder.Entity<Books>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreID)
                .OnDelete(DeleteBehavior.Cascade);
            // one book to many book copies
            modelBuilder.Entity<BookCopies>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(bc => bc.BookID)
                .OnDelete(DeleteBehavior.Cascade);
            // one book copy to many loans
            modelBuilder.Entity<Loans>()
                .HasOne(l => l.BookCopy)
                .WithMany(bc => bc.Loans)
                .HasForeignKey(l => l.CopyID)
                .OnDelete(DeleteBehavior.Cascade);
            // one student to many loans
            modelBuilder.Entity<Loans>()
                .HasOne(l => l.Student)
                .WithMany(s => s.Loans)
                .HasForeignKey(l => l.StudentID)
                .OnDelete(DeleteBehavior.Cascade);
            // one faculty to many students
            modelBuilder.Entity<Students>()
                .HasOne(s => s.Faculty)
                .WithMany(f => f.Students)
                .HasForeignKey(s => s.FacultyID)
                .OnDelete(DeleteBehavior.Cascade);
            // Seed data
            DataSeeder.SeedData(modelBuilder);
        }
    }
}
