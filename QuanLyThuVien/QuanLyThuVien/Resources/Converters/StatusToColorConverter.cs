using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;   

namespace QuanLyThuVien.Resources.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            // Nền của "chip" trạng thái (màu pastel)
            => value?.ToString() == "Active"
               ? (object)new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E7F8EF")) // xanh nhạt
               : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFF1F5"));        // xám nhạt

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}