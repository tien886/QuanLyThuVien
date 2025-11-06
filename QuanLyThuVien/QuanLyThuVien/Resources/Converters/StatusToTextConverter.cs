using System.Globalization;
using System.Windows.Data;
namespace QuanLyThuVien.Resources.Converters;
public class StatusToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value?.ToString() == "Active" ? "Hoạt động" : "Vô hiệu hóa";

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
