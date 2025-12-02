using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.DTOs;
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
            return await _dataContext.Loans.CountAsync(l => l.LoanStatus == "-1");
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
                .Where(l => l.LoanStatus == "0" || l.LoanStatus == "-1")
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
        public async Task DeleteLoanAsync(Loans loan)
        {
            _dataContext.Loans.Remove(loan);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoanTrendStats>> GetLoanTrendsAsync()
        {
            var result = new List<LoanTrendStats>();
            var numOfMonths = 6;

            // Duyệt qua 6 tháng gần nhất (từ 5 tháng trước đến tháng hiện tại)
            for (int i = numOfMonths - 1; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var month = date.Month;
                var year = date.Year;

                // 1. Đếm số lượng MƯỢN trong tháng này
                // Logic: Dựa vào LoanDate
                int borrowed = await _dataContext.Loans
                    .AsNoTracking()
                    .CountAsync(l => l.LoanDate.Month == month && l.LoanDate.Year == year);

                // 2. Đếm số lượng TRẢ trong tháng này
                // Logic: Dựa vào ReturnDate VÀ Trạng thái đã trả (giả sử Status="1" là đã trả/hoàn thành)
                // Lưu ý: Bạn cần kiểm tra lại logic "Đã trả" trong Database của bạn. 
                // Ở đây tôi giả định LoanStatus="1" hoặc kiểm tra ReturnDate có giá trị hợp lệ.
                int returned = await _dataContext.Loans
                    .AsNoTracking()
                    .CountAsync(l => l.LoanStatus == "1" && l.ReturnDate.Month == month && l.ReturnDate.Year == year);

                result.Add(new LoanTrendStats
                {
                    Month = month,
                    Year = year,
                    BorrowedCount = borrowed,
                    ReturnedCount = returned
                });
            }

            return result;
        }
        public async Task<IEnumerable<CategoryLoanStats>> GetLoanStatsByCategoryAsync()
        {
            // Logic: Group by Tên thể loại và đếm số lượng phiếu mượn
            var stats = await _dataContext.Loans
                .Include(l => l.BookCopy)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.BookCategory)
                .Where(l => l.BookCopy.Book.BookCategory != null) // Đảm bảo không null
                .GroupBy(l => l.BookCopy.Book.BookCategory.CategoryName)
                .Select(g => new CategoryLoanStats
                {
                    CategoryName = g.Key,
                    LoanCount = g.Count()
                })
                .OrderByDescending(x => x.LoanCount) // Sắp xếp từ cao xuống thấp cho đẹp
                .Take(7) // Chỉ lấy top 7 thể loại phổ biến nhất
                .AsNoTracking()
                .ToListAsync();

            return stats;
        }
        public async Task<IEnumerable<BookLoanStats>> GetTopBorrowedBooksAsync()
        {
            // Logic: Join Loans -> BookCopy -> Book
            // Group theo Tên sách -> Đếm -> Sắp xếp giảm dần -> Lấy 5 dòng đầu
            return await _dataContext.Loans
                .Include(l => l.BookCopy)
                    .ThenInclude(bc => bc.Book)
                .Where(l => l.BookCopy != null && l.BookCopy.Book != null)
                .GroupBy(l => l.BookCopy.Book.Title)
                .Select(g => new BookLoanStats
                {
                    Title = g.Key,
                    LoanCount = g.Count()
                })
                .OrderByDescending(x => x.LoanCount)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Loans>> GetRecentLoansAsync(int count)
        {
            return await _dataContext.Loans
                .Include(l => l.Student)
                .Include(l => l.BookCopy.Book)
                .OrderByDescending(l => l.LoanDate) // Sắp xếp giảm dần theo ngày mượn
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task UpdateLoan(Loans loan)
        {
            _dataContext.Loans.Update(loan);
            await _dataContext.SaveChangesAsync();
        }
    }
}
