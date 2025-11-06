using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class StudentSeeder
    {
        public static void SeedStudent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>().HasData(
                new Students
                {
                    StudentId = 1,
                    StudentName = "Nguyễn Văn A",
                    Email = "nguyenvana@example.com",
                    PhoneNumber = "0901234567",
                    AccountStatus = "Active",
                    RegistrationDate = new DateTime(2023, 9, 1),
                },
                new Students
                {
                    StudentId = 2,
                    StudentName = "Trần Thị B",
                    Email = "tranthib@example.com",
                    PhoneNumber = "0912345678",
                    AccountStatus = "Disabled",
                    RegistrationDate = new DateTime(2022, 12, 15),
                },
                new Students
                {
                    StudentId = 3,
                    StudentName = "Lê Văn C",
                    Email = "levanc@example.com",
                    PhoneNumber = "0923456789",
                    AccountStatus = "Active",
                    RegistrationDate = new DateTime(2024, 3, 10),
                },
                new Students
                {
                    StudentId = 4,
                    StudentName = "Phạm Thị D",
                    Email = "phamthid@example.com",
                    PhoneNumber = "0934567890",
                    AccountStatus = "Disabled",
                    RegistrationDate = new DateTime(2023, 6, 20),
                },
                new Students
                {
                    StudentId = 5,
                    StudentName = "Hoàng Văn E",
                    Email = "hoangvane@example.com",
                    PhoneNumber = "0945678901",
                    AccountStatus = "Active",
                    RegistrationDate = new DateTime(2025, 1, 5),
                }
            );
        }
    }
}
