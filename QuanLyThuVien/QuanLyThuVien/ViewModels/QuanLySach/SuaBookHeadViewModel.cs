using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;

namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class SuaBookHeadViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBookService _bookService;
        private readonly IBookCategoryService _bookCategoryService;
        private readonly IGenreService _genreService;
        private Books currentBook;
        public SuaBookHeadViewModel(
            IServiceProvider serviceProvider,
            IBookService bookService,
            IBookCategoryService bookCategoryService,
            IGenreService genreService
            )
        {
            _serviceProvider = serviceProvider;
            _bookService = bookService;
            _bookCategoryService = bookCategoryService;
            _genreService = genreService;
        }
        [ObservableProperty]
        private string tenSach;
        [ObservableProperty]
        private string tacGia;
        [ObservableProperty]
        private string iSBN;
        [ObservableProperty]
        private Genres loaiSachSelected;
        [ObservableProperty]
        private ObservableCollection<Genres> loaiSachs;
        [ObservableProperty]
        private BookCategories theLoaiSelected;
        [ObservableProperty]
        private ObservableCollection<BookCategories> theLoais;
        [ObservableProperty]
        private int namXB;
        [ObservableProperty]
        private string nXB;
        [ObservableProperty]
        private string moTa;
        public async Task LoadPage(Books book)
        {
            currentBook = book;
            var genres = await _genreService.GetAllGenresAsync();
            LoaiSachs = new ObservableCollection<Genres>(genres);
            var bookCategories = await _bookCategoryService.GetAllBookCategoriesAsync();
            TheLoais = new ObservableCollection<BookCategories>(bookCategories);
            TenSach = currentBook.Title;
            TacGia = currentBook.Author;
            ISBN = currentBook.ISBN;
            LoaiSachSelected = LoaiSachs.FirstOrDefault(g => g.GenreID == currentBook.GenreID);
            TheLoaiSelected = TheLoais.FirstOrDefault(c => c.CategoryID == currentBook.CategoryID);
            Debug.WriteLine($"LoaiSachSelected: {LoaiSachSelected?.GenreName}, TheLoaiSelected : {TheLoaiSelected?.CategoryName}");
            NamXB = currentBook.PublicationYear;
            NXB = currentBook.Publisher;
            MoTa = currentBook.Description;
        }
        [RelayCommand]
        public async Task SaveChanges()
        {
            currentBook.Title = TenSach;
            currentBook.Author = TacGia;
            currentBook.ISBN = ISBN;
            currentBook.PublicationYear = NamXB;
            currentBook.Description = MoTa;
            currentBook.Publisher = NXB;
            currentBook.BookCategory = TheLoaiSelected;
            currentBook.CategoryID= TheLoaiSelected.CategoryID;
            currentBook.Genre = LoaiSachSelected;
            currentBook.GenreID = LoaiSachSelected.GenreID;
            await _bookService.UpdateBookAsync(currentBook);
            MessageBox.Show("Cập nhật thành công!", "Thông báo");
            await ClosePopup();
        }
        [RelayCommand]
        public async Task ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
}
