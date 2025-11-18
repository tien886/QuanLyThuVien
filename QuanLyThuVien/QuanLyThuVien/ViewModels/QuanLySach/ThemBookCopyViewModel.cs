using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels.QuanLySachPopup;
using QuanLyThuVien.Views.QuanLySachPopup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;


namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class ThemBookCopyViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBookCopyService _bookCopyService;
        private Books currentBook;
        [ObservableProperty]
        private string location;
        [ObservableProperty]
        private string copyID;
        public ThemBookCopyViewModel(
            IServiceProvider serviceProvider,
            IBookCopyService bookCopyService
            )
        {
            _serviceProvider = serviceProvider;
            _bookCopyService = bookCopyService;
        }
        public async Task SetCurrentBook(Books book)
        {
            currentBook = book;
            if(currentBook == null)
            {
                Debug.WriteLine("Book transmission failed");
            }
            else
            {
                CopyID = await _bookCopyService.GetNextAvailableBookCopyID();
                Debug.WriteLine($"add a {currentBook.Title} copies");
            }
        }
        [RelayCommand]
        public async Task AddBookCopies()
        {
            BookCopies bookCopies = new BookCopies
            {
                CopyID = await _bookCopyService.GetNextAvailableBookCopyID(),
                BookID = currentBook.BookID,
                Status = "1",
                DateAdded = DateTime.Now
            };
            await _bookCopyService.AddBookCopiesAsync(bookCopies);
            await ClosePopup();
        }
        [RelayCommand]
        public async Task ClosePopup()
        {
            var window = Application.Current.Windows
             .OfType<ThemBookCopyPopup>()
             .FirstOrDefault(w => w.IsActive);

            window?.Close();

        }
    }
}
