using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class StudentSeeder
    {
        // Vùng dữ liệu ngẫu nhiên
        private static readonly Random random = new Random();

        // Dùng tên không dấu để dễ tạo email
        private static readonly string[] LastNames = {
            "Nguyen", "Tran", "Le", "Pham", "Hoang", "Huynh",
            "Vo", "Phan", "Dang", "Bui", "Do", "Vu"
        };
        private static readonly string[] MaleFirstNames = {
            "Van Tung", "Minh Quan", "Duc Huy", "Anh Khoa",
            "Tuan Kiet", "Gia Bao", "Thanh Dat", "Quoc Tuan"
        };
        private static readonly string[] FemaleFirstNames = {
            "Phuong Thao", "Ngoc Anh", "Gia Han", "Khanh Linh",
            "Minh Chau", "Bao Ngoc", "Hong Anh", "Thu Uyen"
        };
        private static readonly string[] Statuses = { "Active", "Disabled" };

        public static void SeedStudent(ModelBuilder modelBuilder)
        {

            int totalStudentsToGenerate = 1000;
            int startIndex = 6; // Bắt đầu ID từ 6 (vì đã có 1-5)

            for (int i = 0; i < totalStudentsToGenerate; i++)
            {
                int currentId = startIndex + i;
                string lastName = LastNames[random.Next(LastNames.Length)];
                string firstName;

                // 50/50 chọn tên Nam hoặc Nữ
                if (random.Next(2) == 0)
                {
                    firstName = MaleFirstNames[random.Next(MaleFirstNames.Length)];
                }
                else
                {
                    firstName = FemaleFirstNames[random.Next(FemaleFirstNames.Length)];
                }

                string studentName = $"{lastName} {firstName}";

                // Tạo email duy nhất từ tên và ID (ví dụ: giabao6@example.com)
                string emailName = firstName.Replace(" ", "").ToLower();
                string email = $"{emailName}{currentId}@example.com";

                // Tạo SĐT ngẫu nhiên
                string phoneNumber = $"09{random.Next(10000000, 99999999)}";

                // Chọn trạng thái ngẫu nhiên
                string status = Statuses[random.Next(Statuses.Length)];

                // Ngày đăng ký ngẫu nhiên trong 4 năm qua
                DateTime registrationDate = DateTime.Now.AddDays(-random.Next(1, 4 * 365));

                modelBuilder.Entity<Students>().HasData(
                    new Students
                    {
                        StudentId = currentId,
                        StudentName = studentName,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        AccountStatus = status,
                        RegistrationDate = registrationDate
                    }
                );
            }
        }
    }
}
