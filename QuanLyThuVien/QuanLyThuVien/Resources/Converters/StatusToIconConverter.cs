using FontAwesome.Sharp;
using System.Globalization;
using System.Windows.Data; 

namespace QuanLyThuVien.Resources.Converters
{
    public class StatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString() == "1"
               ? IconChar.Ban : IconChar.CheckCircle;      // xanh lá

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}