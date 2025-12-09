using System.Windows.Input;

namespace QuanLyThuVien.ViewModels
{
    public interface IHeaderActionViewModel
    {
        ICommand HeaderButtonCommand { get; } // Lệnh kích hoạt khi bấm nút
        string HeaderButtonLabel { get; }    // Tên nút
        bool IsHeaderButtonVisible { get; } // Có hiện nút không
    }
}