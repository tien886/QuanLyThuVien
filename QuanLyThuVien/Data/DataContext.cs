using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<BookCategories> BookCategories { get; set; }
        public DbSet<BookCopies> BookCopies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // one category to many books
            modelBuilder.Entity<Books>()
                .HasOne(b => b.BookCategory)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);
            // one book to many book copies
            modelBuilder.Entity<BookCopies>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(bc => bc.BookID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
