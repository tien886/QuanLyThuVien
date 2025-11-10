using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public class StudentRepository : IStudentService
    {
        private readonly DataContext _dataContext;
        public StudentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //public async Task<IEnumerable<Students>> GetAllStudentsAsync()
        //{
        //    return await _dataContext.Students
        //        .Include(s => s.Loans)
        //        .ToListAsync();
        //}
        public async Task<IEnumerable<Students>> GetAllStudentsAsync(string? keyword = null)
        {
            var q = _dataContext.Students.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                q = q.Where(s =>
                    EF.Functions.Like(s.StudentName!, $"%{keyword}%") ||
                    EF.Functions.Like(s.Email!, $"%{keyword}%") ||
                    EF.Functions.Like(s.StudentId.ToString(), $"%{keyword}%"));
            }

            return await q.OrderBy(s => s.StudentName).ToListAsync();
        }

        public async Task AddStudentAsync(Students s)
        {
            _dataContext.Students.Add(s);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Students s)
        {
            _dataContext.Students.Update(s);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var s = await _dataContext.Students.FindAsync(id);
            if (s is null)
                return;
            _dataContext.Students.Remove(s);
            await _dataContext.SaveChangesAsync();
        }

        public async Task BlockStudentAsync(int id)
        {
            var s = await _dataContext.Students.FindAsync(id);
            if (s is null)
                return;
            s.AccountStatus = "Vô hiệu hóa";
            await _dataContext.SaveChangesAsync();
        }

        public async Task UnblockStudentAsync(int id)
        {
            var s = await _dataContext.Students.FindAsync(id);
            if (s is null)
                return;
            s.AccountStatus = "Hoạt động";
            await _dataContext.SaveChangesAsync();
        }
        //public async Task ChangeStatus(Students students)
        //{
        //    var s = await _dataContext.Students.FindAsync(students.StudentId);
        //    if (s is null)
        //        return;
        //    students.AccountStatus = students.AccountStatus == "Active" ? "Disabled" : "Active";
        //    Debug.WriteLine($"Sinh viên {students.StudentName} hiện có trạng thái: {students.AccountStatus} trong repo");

        //    await _dataContext.SaveChangesAsync();
        //}
    }
}
