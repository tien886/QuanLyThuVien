using FontAwesome.Sharp;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;   // ✅

namespace QuanLyThuVien.Resources.Converters
{
    public class StatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString() == "Active"
               ? IconChar.Ban : IconChar.CheckCircle;      // xanh lá

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}