using System.Windows.Media;

namespace QuanLyThuVien.DTOs
{
    public class ActivityDisplayItem
    {
        public string Description { get; set; } // Vd: "Nguyễn Văn A mượn sách"
        public string TimeAgo { get; set; }     // Vd: "5 phút trước"
        public string Icon { get; set; }        // Tên Icon FontAwesome (Vd: "Book")

        // Màu sắc cho Icon và Nền Icon
        public SolidColorBrush IconColor { get; set; }
        public SolidColorBrush BackgroundColor { get; set; }
    }
}
