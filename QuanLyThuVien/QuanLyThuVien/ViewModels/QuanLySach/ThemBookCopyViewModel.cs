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
        private readonly Books _currentBook; // Lưu đầu sách cha để biết thêm bản sao cho sách nào

        [ObservableProperty]
        private Locations _selectedLocation;

        [ObservableProperty]
        private ObservableCollection<Locations> _locations = new();

        [ObservableProperty]
        private string _bookCopyID;

        public ThemBookCopyViewModel(
            Books book,
            IBookCopyService bookCopyService,
            ILocationService locationService)
        {
            _currentBook = book;
            _bookCopyService = bookCopyService;
            _locationService = locationService;

            BookCopyID = _bookCopyService.GetNextAvailableBookCopyID().GetAwaiter().GetResult();
            LoadData();
        }

        private async void LoadLocations()
        {
            var list = await _locationService.GetAllLocationsAsync();
            Locations.Clear();
            foreach (var location in list)
            {
                Locations.Add(location);
            }

            if (Locations.Count > 0)
            {
                SelectedLocation = Locations[0];
            }
        }

        // Load danh sách Location từ Database
        private async void LoadData()
        {
            LoadLocations();

            if (_currentBook != null)
            {
                BookCopyID = await _bookCopyService.GetNextAvailableBookCopyID();
            }
        }
        

        [RelayCommand]
        public async Task AddBookCopies()
        {
            if (SelectedLocation == null)
            {
                MessageBox.Show("Vui lòng chọn vị trí!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_currentBook == null) 
                return;

            var newBookCopy = new BookCopies
            {
                CopyID = BookCopyID, // ID đã sinh ở trên
                BookID = _currentBook.BookID,
                Status = "1", // Mặc định: 1 = Có sẵn
                LocationID = SelectedLocation.LocationID,
                DateAdded = DateTime.Now
            };

            try
            {
                // Lưu xuống Database
                await _bookCopyService.AddBookCopiesAsync(newBookCopy);
                // Trên DataGrid, bạn có cột "Vị trí" đang Binding vào Location.LocName. -> Nếu không có thể đéo có name của location đâu 
                newBookCopy.Location = SelectedLocation;
                //WeakReferenceMessenger.Default.Send(new BookCopyAddedMessage(newBookCopy));

                MessageBox.Show("Thêm bản sao thành công!", "Thông báo");
                ClosePopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
    public class BookCopyAddedMessage
    {
        public BookCopies NewBookCopy { get; }
        public BookCopyAddedMessage(BookCopies bookCopy) => NewBookCopy = bookCopy;
    }
}
