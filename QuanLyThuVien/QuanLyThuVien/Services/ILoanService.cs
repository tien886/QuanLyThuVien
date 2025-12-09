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
        // Cho quản lý mượn trả
        Task<IEnumerable<Loans>> GetAllLoansAsync();


        // Thêm, sửa phiếu mượn
        Task UpdateLoan(Loans loan);
        Task AddLoan(Loans loan);


        // Lấy số liệu tổng quát cho các badges (Dashboard)
        Task<int> GetCurrentlyBorrowedBooksAsync();
        Task<int> GetCurrentBorrowingStudentsAsync();
        Task<IEnumerable<OverdueBookStats>> GetOverdueBooksAsync(int count);
        Task<int> GetDaTraTheoThang(DateTime present);


        // Lấy data cho thống kê phiếu mượn theo sinh viên (Quản lý sinh viên)
        Task<StudentLoanStats> GetLoanStatsByStudentIdAsync(int studentId);
        //Task<IEnumerable<LoanTrendStats>> GetLoanTrendsAsync();
        Task<IEnumerable<LoanTrendStats>> GetLoanTrendsAsync(DateTime startDate, DateTime endDate);


        // Lấy data cho thống kê thể loại sách phổ biến (Dashboard)
        Task<IEnumerable<CategoryLoanStats>> GetLoanStatsByCategoryAsync(int top);


        // Lấy data cho thống kê sách được mượn nhiều nhất (Dashboard)
        Task<IEnumerable<BookLoanStats>> GetTopBorrowedBooksAsync(int top);


        // Lấy data cho thống kê phiếu mượn gần đây (Dashboard)
        Task<IEnumerable<Loans>> GetRecentLoansAsync(int count);


        // Lấy data cho thống kê sách quá hạn (Dashboard)
        Task<int> GetOverdueBooksAsyncCount();

        // Phân trang 
        Task<int> GetTotalPages(int pageSize, string keyword = "");
        Task<IEnumerable<Loans>> GetLoansPage(int pageIndex, int pageSize, string keyword = "");

        // Nhận trả sách 
        Task ReturnBookTransactionAsync(Loans loan, string newCopyStatus);
    }
}
