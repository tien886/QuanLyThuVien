using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels.QuanLySachPopup;
using QuanLyThuVien.Views.QuanLySachPopup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; 

namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class BookDetailAndCopyViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBookCopyService _bookService;
        private Books currentBook;
        public BookDetailAndCopyViewModel(
            IServiceProvider serviceProvider,
            IBookCopyService bookService
            )
        {
            _serviceProvider = serviceProvider;
            _bookService = bookService;

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
        [ObservableProperty]
        private string moTa;
        [ObservableProperty]
        private int tongSoBan;
        [ObservableProperty]
        private int coSan;
        [ObservableProperty]
        private int dangMuon;
        [ObservableProperty]
        private int hongMat;
        [ObservableProperty]
        private ObservableCollection<BookCopies> bookCopiesList = new();

        public async Task LoadPage(Books book)
        {
            currentBook = book;
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
                MoTa = currentBook.Description;
                // badget show up
                TongSoBan = currentBook.TotalCopies;
                CoSan = currentBook.AvailableCount;
                DangMuon = currentBook.BookCopies.Count(c => c.Status == "0");
                HongMat = TongSoBan - CoSan - DangMuon;
                // copies list 
                BookCopiesList = new ObservableCollection<BookCopies>(await _bookService.GetAllBookCopiesAsync(book));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi tải trang: {ex.Message}");
            }
            ;
        }
        [RelayCommand]
        public async Task ClosePopup()
        {
            var window = Application.Current.Windows
            .OfType<BookDetailAndCopyPopup>()
            .FirstOrDefault(w => w.IsActive);

            window?.Close();
        }
        [RelayCommand]
        public async Task AddCopies()
        {
            var themBookCopyPopup = _serviceProvider.GetRequiredService<ThemBookCopyPopup>();
            if(themBookCopyPopup.DataContext is ThemBookCopyViewModel vm)
            {
                await vm.SetCurrentBook(currentBook);
            }
            await LoadPage(currentBook);
            themBookCopyPopup.ShowDialog();
        }
    }
}
