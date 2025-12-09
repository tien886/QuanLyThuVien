using CommunityToolkit.Mvvm.ComponentModel;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class BookItemViewModel: ObservableObject
    {
        private readonly Books _bookModel;
        public Books Book => _bookModel;

        [ObservableProperty]
        private int _maDauSach;
        [ObservableProperty]
        private string _tenSach;
        [ObservableProperty]
        private string _tacGia;
        [ObservableProperty]
        private string _iSBN;
        [ObservableProperty]
        private string _theLoai;
        [ObservableProperty]
        public int tongBanSao;
        [ObservableProperty]
        public int coSan;

        public BookItemViewModel(Books book)
        {
            _bookModel = book;

            MaDauSach = book.BookID;
            TenSach = book.Title;
            TacGia = book.Author;
            ISBN = book.ISBN;
            TheLoai = book.BookCategory?.CategoryName ?? "Chưa cập nhật";

            // Tận dụng book đã load sẵn BookCopies => không cần gọi lại dịch vụ
            if (book.BookCopies != null)
            {
                TongBanSao = book.BookCopies.Count;
                coSan = book.BookCopies.Count(c => c.Status == "1");
            }
        }
        
        public void RefreshFromModel(Books updatedModel)
        {
            // Cập nhật lên Properties để UI tự động thay đổi (NotifyPropertyChanged)
            MaDauSach = updatedModel.BookID;
            TenSach = updatedModel.Title;
            TacGia = updatedModel.Author;
            ISBN = updatedModel.ISBN;
            TheLoai = updatedModel.BookCategory?.CategoryName ?? "Chưa cập nhật";
        }
    }
}
