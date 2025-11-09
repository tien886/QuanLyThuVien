
using QuanLyThuVien.ViewModels.QuanLySach;
using System.Windows;

namespace QuanLyThuVien.Views.QuanLySachPopup
{
    /// <summary>
    /// Interaction logic for ThemBookCopyPopup.xaml
    /// </summary>
    public partial class ThemBookCopyPopup : Window
    {
        public ThemBookCopyPopup(ThemBookCopyViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        
    }
}
