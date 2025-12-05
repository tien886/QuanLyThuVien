using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class ThemBookCopyViewModel : ObservableObject
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly ILocationService _locationService;
        private Books currentBook;
        [ObservableProperty]
        private Locations locationSelected;
        [ObservableProperty]
        private ObservableCollection<Locations> locations;
        [ObservableProperty]
        private string copyID;
        public ThemBookCopyViewModel(
            IBookCopyService bookCopyService,
            ILocationService locationService
            )
        {
            _bookCopyService = bookCopyService;
            _locationService = locationService;
        }
        public async Task SetCurrentBook(Books book)
        {
            var locs = await _locationService.GetAllLocationsAsync();
            Locations = new ObservableCollection<Locations>(locs);
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
                LocationID = LocationSelected.LocationID,
                DateAdded = DateTime.Now
            };
            await _bookCopyService.AddBookCopiesAsync(bookCopies);
            MessageBox.Show("Sửa bản sao thành công!", "Thông báo");
            await ClosePopup();
        }
        [RelayCommand]
        public async Task ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
}
