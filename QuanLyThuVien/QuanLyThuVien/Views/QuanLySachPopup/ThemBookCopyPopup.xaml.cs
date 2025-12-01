
using QuanLyThuVien.ViewModels.QuanLySach;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyThuVien.Views.QuanLySachPopup
{
    /// <summary>
    /// Interaction logic for ThemBookCopyPopup.xaml
    /// </summary>
    public partial class ThemBookCopyPopup : UserControl 
    {
        public ThemBookCopyPopup(ThemBookCopyViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }


    }
}
