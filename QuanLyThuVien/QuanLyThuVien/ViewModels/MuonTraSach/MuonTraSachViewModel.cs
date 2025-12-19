//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using CommunityToolkit.Mvvm.Messaging;
//using Microsoft.Extensions.DependencyInjection;
//using QuanLyThuVien.Models;
//using QuanLyThuVien.Services;
//using QuanLyThuVien.ViewModels.MuonTraSach;
//using QuanLyThuVien.Views.MuonTraSachPopup;
//using System.Collections.ObjectModel;
//using System.Diagnostics;

//namespace QuanLyThuVien.ViewModels
//{
//    public partial class MuonTraSachViewModel : ObservableObject
//    {
//        private IServiceProvider _serviceProvider;
//        private ILoanService _loanService;

//        public MuonTraSachViewModel(
//            IServiceProvider serviceProvider
//            , ILoanService loanService
//            )
//        {
//            _serviceProvider = serviceProvider;
//            _loanService = loanService;
//            _ = LoadData();
//        }
//        private string _searhBarText = "";
//        public string SearchBarText
//        {
//            get => _searhBarText;
//            set
//            {
//                if (_searhBarText != value)
//                {
//                    _searhBarText = value;
//                    OnPropertyChanged();
//                    _ = SearchPhieuMuonAsync(SearchBarText);
//                    Task.Delay(1000);
//                }
//            }
//        }
//        [ObservableProperty]
//        private int dangMuon;
//        [ObservableProperty]
//        private int quaHan;
//        [ObservableProperty]
//        private int daTraThangNay;
//        [ObservableProperty]
//        private ObservableCollection<Loans> loanList = new();
//        private async Task LoadData()
//        {
//            DangMuon = await _loanService.GetCurrentlyBorrowedBooksAsync();
//            QuaHan = await _loanService.GetOverdueBooksAsyncCount();
//            DaTraThangNay = await _loanService.GetDaTraTheoThang(DateTime.Now);
//            LoanList = new ObservableCollection<Loans>(await _loanService.GetAllLoansAsync());
//        }
//        public async Task SearchPhieuMuonAsync(string hint)
//        {
//            try
//            {
//                if(hint == "")
//                {
//                    return;
//                }
//                var loans = await _loanService.GetAllLoansAsync();
//                ObservableCollection<Loans> filteredLoans = [];
//                foreach(Loans loan in loans)
//                {
//                    string pseudoHint = hint.ToLower();
//                    string pseudoMaPhieu = loan.LoanID.ToString();
//                    string pseudoStudentName = loan.Student.StudentName.ToLower();
//                    string pseudoBook = loan.BookCopy.Book.Title.ToLower();
//                    string pseudoBorrowDate = loan.LoanDate.ToString();
//                    string pseudoDueDate= loan.DueDate.ToString();
//                    if (pseudoMaPhieu.Contains(pseudoHint) || pseudoStudentName.Contains(pseudoHint) || pseudoBook.Contains(pseudoHint) || pseudoBorrowDate.Contains(pseudoHint) || pseudoDueDate.Contains(pseudoHint))
//                        filteredLoans.Add(loan);
//                }
//                LoanList = filteredLoans;
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex);
//            }
//        }
//        [RelayCommand]
//        public async Task ReturnBook(Loans loan)
//        {
//            var nhanTraSachPopup = _serviceProvider.GetRequiredService<NhanTraSachPopup>();
//            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(nhanTraSachPopup));
//            if (nhanTraSachPopup.DataContext is NhanTraSachViewModel vm)
//            {
//                vm.SetCurrentLoan(loan);
//            }
//        }

//    }
//}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels.MuonTraSach;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QuanLyThuVien.ViewModels
{
    public partial class MuonTraSachViewModel : ObservableObject, IHeaderActionViewModel
    {
        // Các service
        private readonly ILoanService _loanService;
        private readonly IBookCopyService _bookCopyService;
        private readonly IStudentService _studentService;

        // Interface Header
        public ICommand HeaderButtonCommand => OpenThemPhieuMuonPopupCommand;
        public string HeaderButtonLabel => "Tạo phiếu mượn";
        public bool IsHeaderButtonVisible => true;

        // Thống kê
        [ObservableProperty] private int _dangMuon;
        [ObservableProperty] private int _quaHan;
        [ObservableProperty] private int _daTraThangNay;

        // Danh sách phiếu mượn hiển thị
        [ObservableProperty]
        private ObservableCollection<LoanItemViewModel> _loanList = new();

        // Tìm kiếm & Phân trang
        [ObservableProperty] private string searchText = "";
        private CancellationTokenSource? _searchCts;

        private int PageSize = 15;
        private const int WindowSize = 5;
        [ObservableProperty] private int currentPage;
        [ObservableProperty] private int totalPages;
        [ObservableProperty] private int firstPage;
        [ObservableProperty] private int secondPage;
        [ObservableProperty] private int thirdPage;
        [ObservableProperty] private int fourthPage;
        [ObservableProperty] private int fifthPage;

        public MuonTraSachViewModel(
            IStudentService studentService, 
            IBookCopyService bookCopyService,
            ILoanService loanService)
        {
            _studentService = studentService;
            _bookCopyService = bookCopyService;
            _loanService = loanService;

            WeakReferenceMessenger.Default.Register<LoanAddedMessage>(this, (r, message) =>
            {
                LoadCurrentPageAsync();
                UpdateStats();
            });
            WeakReferenceMessenger.Default.Register<LoanDeletedMessage>(this, (r, message) =>
            {
                LoadCurrentPageAsync();
            });

            _ = LoadDataAsync();
        }

        [RelayCommand]
        private async Task LoadDataAsync()
        {
            await UpdateStats();
            await SearchAsync(); 
        }

        private async Task UpdateStats()
        {
            DangMuon = await _loanService.GetCurrentlyBorrowedBooksAsync();
            QuaHan = await _loanService.GetOverdueBooksAsyncCount();
            DaTraThangNay = await _loanService.GetDaTraTheoThang(DateTime.Now);
        }


        // Thao tác tim kiếm
        async partial void OnSearchTextChanged(string value)
        {
            _searchCts?.Cancel();
            _searchCts = new CancellationTokenSource();
            try
            {
                await Task.Delay(350, _searchCts.Token);
                await SearchAsync();
            }
            catch (TaskCanceledException) { }
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            CurrentPage = 1;
            TotalPages = await _loanService.GetTotalPages(PageSize, SearchText);
            UpdateWindow();
            await LoadCurrentPageAsync();
        }

        private async Task LoadCurrentPageAsync()
        {
            if (TotalPages == 0) { LoanList.Clear(); return; }
            if (CurrentPage < 1) CurrentPage = 1;

            var loans = await _loanService.GetLoansPage(CurrentPage - 1, PageSize, SearchText);
            LoanList.Clear();
            foreach (var loan in loans)
            {
                LoanList.Add(new LoanItemViewModel(loan));
            }
        }

        // Thao tác mở popup Thêm phiếu mượn
        [RelayCommand]
        private void OpenThemPhieuMuonPopup()
        {
            var addPhieuMuonVM = new ThemPhieuMuonViewModel(_studentService, _bookCopyService, _loanService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(addPhieuMuonVM));
        }

        [RelayCommand]
        private void OpenNhanTraSachPopup(LoanItemViewModel loanVM)
        {
            var nhanTraVM = new NhanTraSachViewModel(loanVM.Loan, _loanService, _bookCopyService);
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(nhanTraVM));

        }


        // Thao tác phân trang 
        [RelayCommand]
        private void PageSelection(int pageNumber)
        {
            if (pageNumber < 0 || pageNumber > TotalPages)
                return;

            CurrentPage = pageNumber;
            UpdateWindow();
            _ = LoadCurrentPageAsync();
        }

        [RelayCommand]
        private async Task GoToPreviousPage()
        {
            if (CurrentPage <= 1)
                return;

            CurrentPage--;
            UpdateWindow();
            await LoadCurrentPageAsync();
        }

        [RelayCommand]
        private async Task GoToNextPage()
        {
            if (CurrentPage >= TotalPages)
                return;

            CurrentPage++;
            UpdateWindow();
            await LoadCurrentPageAsync();
        }

        private void UpdateWindow()
        {
            if (TotalPages <= 1)
                return;

            int start = CurrentPage - WindowSize / 2;
            if (start < 1)
                start = 1;

            int end = start + WindowSize - 1;
            if (end > TotalPages)
            {
                end = TotalPages;
                start = Math.Max(1, end - WindowSize + 1);
            }

            FirstPage = start;
            SecondPage = start + 1 <= end ? start + 1 : 0;
            ThirdPage = start + 2 <= end ? start + 2 : 0;
            FourthPage = start + 3 <= end ? start + 3 : 0;
            FifthPage = start + 4 <= end ? start + 4 : 0;
        }
    }
}
