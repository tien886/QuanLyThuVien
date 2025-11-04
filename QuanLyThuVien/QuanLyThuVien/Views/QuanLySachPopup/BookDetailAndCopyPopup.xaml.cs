using QuanLyThuVien.ViewModels.QuanLySach;
using System.Windows;
namespace QuanLyThuVien.ViewModels.QuanLySachPopup
{
    /// <summary>
    /// Interaction logic for BookDetailAndCopyPopup.xaml
    /// </summary>
    public partial class BookDetailAndCopyPopup : Window
    {
        public BookDetailAndCopyPopup(BookDetailAndCopyViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

    }
}
