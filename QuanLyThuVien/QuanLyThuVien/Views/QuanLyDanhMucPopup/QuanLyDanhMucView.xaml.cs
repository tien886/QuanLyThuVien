using QuanLyThuVien.ViewModels;
using System.Windows.Controls;


namespace QuanLyThuVien.Views
{
    /// <summary>
    /// Interaction logic for QuanLyDanhMucView.xaml
    /// </summary>
    public partial class QuanLyDanhMucView : UserControl
    {
        public QuanLyDanhMucView(QuanLyDanhMucViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
