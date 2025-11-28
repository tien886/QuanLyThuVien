using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace QuanLyThuVien.Resources.Converters
{
    public class PageSelectionColour : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return false;

            if (values[0] == null || values[1] == null)
                return false;

            if (int.TryParse(values[0].ToString(), out int currentPage) &&
                int.TryParse(values[1].ToString(), out int thisPage))
            {
                return currentPage == thisPage;
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
