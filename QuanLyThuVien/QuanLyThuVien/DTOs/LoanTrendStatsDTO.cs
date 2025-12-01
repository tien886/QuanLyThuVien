namespace QuanLyThuVien.DTOs
{
    // DTO cho thống kê xu hướng mượn trả sách theo tháng
    public class LoanTrendStats
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int BorrowedCount { get; set; } // Số lượng mượn
        public int ReturnedCount { get; set; } // Số lượng trả
    }
}
