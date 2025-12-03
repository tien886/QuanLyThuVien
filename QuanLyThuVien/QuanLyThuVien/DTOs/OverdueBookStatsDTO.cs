namespace QuanLyThuVien.DTOs
{
    public class OverdueBookStats
    {
        public string BookTitle { get; set; }
        public string ReaderName { get; set; }
        public DateTime DueDate { get; set; } // Ngày hết hạn
        public int DaysOverdue { get; set; }  // Số ngày quá hạn
    }
}
