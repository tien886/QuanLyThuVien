using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public class LoanRepository : ILoanService
    {
        private readonly DataContext _dataContext;
        public LoanRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<int> GetDangMuon()
        {
            return await _dataContext.Loans.CountAsync(l => l.LoanStatus == "0");
        }
        public async Task<int> GetQuaHan()
        {
            return await _dataContext.Loans.CountAsync(l => l.LoanStatus == "0" && l.ReturnDate > l.DueDate);
        }
        public async Task<int> GetDaTraTheoThang(DateTime present)
        {
            int month = present.Month;
            int year = present.Year;

            return await _dataContext.Loans
                .CountAsync(l =>
                    l.LoanStatus == "1" &&
                    l.ReturnDate.Month == month &&
                    l.ReturnDate.Year == year
                );
        }
        public async Task<IEnumerable<Loans>> GetAllLoansAsync()
        {
            return await _dataContext.Loans
                .Include(s => s.Student)
                .Include(b => b.BookCopy.Book)
                .ToListAsync();
        }
        public async Task<StudentLoanStats> GetLoanStatsByStudentIdAsync(int studentId)
        {
            // Lấy tất cả phiếu mượn của sinh viên này (Query 1 lần)
            var studentLoans = await _dataContext.Loans
                                    .Where(l => l.StudentID == studentId)
                                    .AsNoTracking()
                                    .ToListAsync();

            return new StudentLoanStats
            {
                // 1. Tổng đã mượn (Tính cả lịch sử)
                TotalBorrowed = studentLoans.Count,

                // 2. Đang mượn (Status = "0" là đang mượn - dựa theo logic cũ của bạn)
                CurrentlyBorrowed = studentLoans.Count(l => l.LoanStatus == "0"),

                // 3. Quá hạn (Đang mượn VÀ Quá hạn trả)
                Overdue = studentLoans.Count(l => l.LoanStatus == "0" && DateTime.Now > l.DueDate)
            };
        }

    }
}
