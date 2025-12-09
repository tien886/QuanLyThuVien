using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Windows;


namespace QuanLyThuVien.ViewModels.QuanLySach
{
    public partial class SuaBookCopyViewModel : ObservableObject
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly ILocationService _locationService;
        private readonly BookCopies _originalBookCopy;

        [ObservableProperty]
        private string _bookCopyID; // Vì ID không thay đổi
        [ObservableProperty]
        private Locations _selectedLocation;
        [ObservableProperty]
        private ObservableCollection<Locations> locations = new();

        public SuaBookCopyViewModel(
            BookCopies bookCopy,
            IBookCopyService bookCopyService,
            ILocationService locationService
            )
        {
            _originalBookCopy = bookCopy;
            _bookCopyService = bookCopyService;
            _locationService = locationService;

            BookCopyID = bookCopy.CopyID;
            SelectedLocation = bookCopy.Location;
            LoadLocatins();
        }

        private async void LoadLocatins()
        {
            var list = await _locationService.GetAllLocationsAsync();
            locations.Clear();
            foreach(var location in list)
            {
                locations.Add(location);
            }
        }

        [RelayCommand]
        public async Task Save()
        {
            if (SelectedLocation == null) return;

            _originalBookCopy.LocationID = SelectedLocation.LocationID;

            var selectedLocationObj = Locations.FirstOrDefault(g => g.LocationID == _selectedLocation.LocationID);
            if (selectedLocationObj != null)
            {
                _originalBookCopy.Location = selectedLocationObj;
            }

            try
            {
                // Lưu thay đổi vào Database và phát tín hiệu cập nhật UI 
                await _bookCopyService.UpdateCopiesAsync(_originalBookCopy);
                //WeakReferenceMessenger.Default.Send(new BookCopyUpdatedMessage(_originalBookCopy)); 
                // Vì khi ta chỉnh sửa và save xong thì nó tự động tắt popup và khi mớ lại BookDetailAndCopy thì như load lại trang nên nó cập nhật
                // Nhưng mà nên giữ lại lỡ sau này cần
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                ClosePopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        [RelayCommand]
        private void ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
    public class BookCopyUpdatedMessage
    {
        public BookCopies UpdatedBookCopy { get; }
        public BookCopyUpdatedMessage(BookCopies bookCopy) => UpdatedBookCopy = bookCopy;
    }
}
