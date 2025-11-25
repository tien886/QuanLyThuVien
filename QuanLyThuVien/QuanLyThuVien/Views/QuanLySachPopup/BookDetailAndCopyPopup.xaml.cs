using QuanLyThuVien.ViewModels.QuanLySach;
using System.Windows;
using System.Windows.Controls;
namespace QuanLyThuVien.ViewModels.QuanLySachPopup
{
    /// <summary>
    /// Interaction logic for BookDetailAndCopyPopup.xaml
    /// </summary>
    public partial class BookDetailAndCopyPopup : UserControl
    {
        public BookDetailAndCopyPopup(BookDetailAndCopyViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
