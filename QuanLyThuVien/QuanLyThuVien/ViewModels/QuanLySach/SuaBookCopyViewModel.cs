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
    public partial class SuaBookCopyViewModel : ObservableObject
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly ILocationService _locationService;
        private BookCopies currentBookCopy;
        [ObservableProperty]
        private Locations locationSelected;
        [ObservableProperty]
        private ObservableCollection<Locations> locations;
        [ObservableProperty]
        private string copyID;
        public SuaBookCopyViewModel(
            IBookCopyService bookCopyService,
            ILocationService locationService
            )
        {
            _bookCopyService = bookCopyService;
            _locationService = locationService;
        }
        public async Task SetCurrentBook(BookCopies bookCopy)
        {
            var locs = await _locationService.GetAllLocationsAsync();
            Locations = new ObservableCollection<Locations>(locs);
            currentBookCopy = bookCopy;
            LocationSelected = await _bookCopyService.GetLocationByBookCopyID(CopyID);
            Debug.WriteLine(LocationSelected.LocName);
            if (currentBookCopy == null)
            {
                Debug.WriteLine("Book transmission failed");
            }
            else
            {
                CopyID = await _bookCopyService.GetNextAvailableBookCopyID();
            }
        }
        [RelayCommand]
        public async Task SaveBookCopies()
        {
            BookCopies bookCopies = new BookCopies
            {
                CopyID = await _bookCopyService.GetNextAvailableBookCopyID(),
                BookID = currentBookCopy.BookID,
                Status = "1",
                LocationID = LocationSelected.LocationID,
                DateAdded = DateTime.Now
            };
            await _bookCopyService.UpdateCopiesAsync(bookCopies);
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
