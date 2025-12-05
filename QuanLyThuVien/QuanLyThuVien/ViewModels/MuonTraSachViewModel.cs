using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace QuanLyThuVien.ViewModels
{
    public partial class MuonTraSachViewModel : ObservableObject
    {
        private IServiceProvider _serviceProvider;
        private ILoanService _loanService;
        public MuonTraSachViewModel(
            IServiceProvider serviceProvider
            , ILoanService loanService
            )
        {
            _serviceProvider = serviceProvider;
            _loanService = loanService;
            _ = LoadData();
        }
        private string _searhBarText = "";
        public string SearchBarText
        {
            get => _searhBarText;
            set
            {
                if (_searhBarText != value)
                {
                    _searhBarText = value;
                    OnPropertyChanged();
                    _ = SearchPhieuMuonAsync(SearchBarText);
                    Task.Delay(1000);
                }
            }
        }
        [ObservableProperty]
        private int dangMuon;
        [ObservableProperty]
        private int quaHan;
        [ObservableProperty]
        private int daTraThangNay;
        [ObservableProperty]
        private ObservableCollection<Loans> loanList = new();
        private async Task LoadData()
        {
            DangMuon = await _loanService.GetCurrentlyBorrowedBooksAsync();
            QuaHan = await _loanService.GetOverdueBooksAsyncCount();
            DaTraThangNay = await _loanService.GetDaTraTheoThang(DateTime.Now);
            LoanList = new ObservableCollection<Loans>(await _loanService.GetAllLoansAsync());
        }
        public async Task SearchPhieuMuonAsync(string hint)
        {
            try
            {
                if(hint == "")
                {
                    return;
                }
                var loans = await _loanService.GetAllLoansAsync();
                ObservableCollection<Loans> filteredLoans = [];
                foreach(Loans loan in loans)
                {
                    string pseudoHint = hint.ToLower();
                    string pseudoMaPhieu = loan.LoanID.ToString();
                    string pseudoStudentName = loan.Student.StudentName.ToLower();
                    string pseudoBook = loan.BookCopy.Book.Title.ToLower();
                    string pseudoBorrowDate = loan.LoanDate.ToString();
                    string pseudoDueDate= loan.DueDate.ToString();
                    if (pseudoMaPhieu.Contains(pseudoHint) || pseudoStudentName.Contains(pseudoHint) || pseudoBook.Contains(pseudoHint) || pseudoBorrowDate.Contains(pseudoHint) || pseudoDueDate.Contains(pseudoHint))
                        filteredLoans.Add(loan);
                }
                LoanList = filteredLoans;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        [RelayCommand]
        public async Task ReturnBook(Loans loan)
        {
            try
            {
                if (loan == null) return;

                var result = System.Windows.MessageBox.Show(
                    $"Bạn có chắc muốn xác nhận trả sách \"{loan.BookCopy.Book.Title}\" từ sinh viên {loan.Student.StudentName} không?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result != MessageBoxResult.Yes)
                    return;
                UpdateLoanAfterReturn(loan);
                await LoadData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        private void UpdateLoanAfterReturn(Loans loan)
        {
            loan.BookCopy.Status = "1";
            loan.ReturnDate = DateTime.Now;
            loan.LoanStatus = "1";
            _loanService.UpdateLoan(loan);
        }
    }
}
