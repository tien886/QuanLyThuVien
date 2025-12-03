using QuanLyThuVien.DTOs;
using QuanLyThuVien.Models;

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
        Task<int> GetDaTraTheoThang(DateTime present);
        Task<IEnumerable<Loans>> GetAllLoansAsync();
        Task<StudentLoanStats> GetLoanStatsByStudentIdAsync(int studentId);
        Task<IEnumerable<LoanTrendStats>> GetLoanTrendsAsync();
        Task<IEnumerable<CategoryLoanStats>> GetLoanStatsByCategoryAsync();
        Task<IEnumerable<BookLoanStats>> GetTopBorrowedBooksAsync();
        Task<IEnumerable<Loans>> GetRecentLoansAsync(int count);
        Task UpdateLoan(Loans loan);
        Task<IEnumerable<OverdueBookStats>> GetOverdueBooksAsync(int count);
        Task<int> GetCurrentlyBorrowedBooksAsync();
        Task<int> GetCurrentBorrowingStudentsAsync();
        Task<int> GetOverdueBooksAsyncCount();
    }
}
