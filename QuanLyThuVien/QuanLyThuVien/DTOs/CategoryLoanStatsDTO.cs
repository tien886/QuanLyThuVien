namespace QuanLyThuVien.DTOs
{
    // DTO cho thống kê số lượng sách mượn theo thể loại
    public class CategoryLoanStats
    {
        public string CategoryName { get; set; }
        public int LoanCount { get; set; }
    }
}
