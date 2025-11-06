using System.Globalization;
using System.Windows.Data;
using System.Windows.Media; // <-- Phải là System.Windows.Media, không phải System.Drawing

namespace QuanLyThuVien.Resources.Converters
{
    public class StatusToIconColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value?.ToString()?.Trim();
            if (string.Equals(status, "Active", StringComparison.OrdinalIgnoreCase))
            {
                return Brushes.Red; 
            }
            return Brushes.Green; 
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}