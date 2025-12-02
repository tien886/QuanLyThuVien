using QuanLyThuVien.DTOs;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Services
{
    public class StudentLoanStats
    {
        public int TotalBorrowed { get; set; }
        public int CurrentlyBorrowed { get; set; }
        public int Overdue { get; set; }
    }

    public interface ILoanService
    {
        Task<int> GetDangMuon();
        Task<int> GetQuaHan();
        Task<int> GetDaTraTheoThang(DateTime present);
        Task<IEnumerable<Loans>> GetAllLoansAsync();
        Task<StudentLoanStats> GetLoanStatsByStudentIdAsync(int studentId);
        Task<IEnumerable<LoanTrendStats>> GetLoanTrendsAsync();
        Task<IEnumerable<CategoryLoanStats>> GetLoanStatsByCategoryAsync();
        Task<IEnumerable<BookLoanStats>> GetTopBorrowedBooksAsync();
        Task<IEnumerable<Loans>> GetRecentLoansAsync(int count);
        Task UpdateLoan(Loans loan);
    }
}
