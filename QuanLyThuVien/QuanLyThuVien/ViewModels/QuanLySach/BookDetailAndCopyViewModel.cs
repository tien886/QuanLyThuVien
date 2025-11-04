using CommunityToolkit.Mvvm.ComponentModel;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class BookDetailAndCopyViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private Books currentBook;
        public BookDetailAndCopyViewModel(
            IServiceProvider serviceProvider
            
            )
        {
            _serviceProvider = serviceProvider;
            
        }
        
        [ObservableProperty]
        private int maDauSach;
        [ObservableProperty]
        private string tenSach;
        [ObservableProperty]
        private string tacGia;
        [ObservableProperty]
        private string iSBN;
        [ObservableProperty]
        private string theLoai;
        [ObservableProperty]
        private string nXB;
        [ObservableProperty]
        private string namXB;

        public async Task LoadPage(Books book)
        {
            currentBook  = book;
            Debug.WriteLine($"Go to loadpage");
            try
            {
                Debug.WriteLine($"book is ok");
                MaDauSach = currentBook.BookID;
                TenSach = currentBook.Title;
                TacGia = currentBook.Author;
                ISBN = currentBook.ISBN;
                TheLoai = currentBook.BookCategory.CategoryName;
                NXB = currentBook.Publisher;
                NamXB = currentBook.PublicationYear.ToString();

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Lỗi khi tải trang: {ex.Message}");
            };
        }
    }
}
