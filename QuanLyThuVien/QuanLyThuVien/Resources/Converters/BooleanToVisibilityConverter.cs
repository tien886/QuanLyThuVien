using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace QuanLyThuVien.Resources.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Chuyển đổi từ bool (true/false) sang Visibility (Visible/Collapsed).
        /// </summary>
        /// <param name="value">Giá trị bool từ ViewModel (ví dụ: IsPopupVisible).</param>
        /// <param name="targetType">Kiểu Visibility.</param>
        /// <param name="parameter">Tham số (không dùng).</param>
        /// <param name="culture">Culture (không dùng).</param>
        /// <returns>Visible nếu 'value' là true, Collapsed nếu 'value' là false.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            // Mặc định là Collapsed nếu có lỗi
            return Visibility.Collapsed;
        }

        /// <summary>
        /// Chuyển đổi ngược (không được hỗ trợ cho kịch bản này).
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Chúng ta không cần ConvertBack (chuyển từ Visibility về bool)
            throw new NotImplementedException();
        }
    }
}
