namespace QuanLyThuVien.DTOs
{
    // DTO cho các sách được mượn nhiều nhất
    public class BookLoanStats
    {
        public string Title { get; set; }
        public int LoanCount { get; set; }
    }
}
