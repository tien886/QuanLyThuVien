using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Windows;
namespace QuanLyThuVien.ViewModels.MuonTraSach
{
    public partial class NhanTraSachViewModel : ObservableObject
    {
        private readonly ILoanService _loanService;
        private readonly IBookCopyService _bookCopyService; // Cần thêm để lấy giá sách nếu cần
        private Loans _currentLoan;

        private const decimal PHAT_QUA_HAN_MOI_NGAY = 5000;
        private const decimal PHAT_MAT_SACH = 50000; 
        private const decimal PHAT_HONG_SACH = 20000;

        public NhanTraSachViewModel(Loans loan, ILoanService loanService, IBookCopyService bookCopyService)
        {
            _loanService = loanService;
            _bookCopyService = bookCopyService;
            StatusOptions = Enum.GetValues(typeof(BookReturnStatus));
            SelectedStatus = BookReturnStatus.OK;

            SetCurrentLoan(loan);
        }

        public enum BookReturnStatus
        {
            OK = 1,     // Bình thường
            Lost = -1,  // Mất
            Broken = -2 // Hỏng
        }

        public Array StatusOptions { get; }

        [ObservableProperty] private BookReturnStatus _selectedStatus;
        [ObservableProperty] private string _studentName;
        [ObservableProperty] private string _bookTitle;
        [ObservableProperty] private DateTime _dueDate;
        [ObservableProperty] private int _overdueDays; 
        [ObservableProperty] private decimal _fineAmount; 
        [ObservableProperty] private string _fineReason;  

        partial void OnSelectedStatusChanged(BookReturnStatus value)
        {
            CalculateFine();
        }

        public void SetCurrentLoan(Loans loan)
        {
            _currentLoan = loan;
            StudentName = loan.Student?.StudentName ?? "";
            BookTitle = loan.BookCopy?.Book?.Title ?? "";
            DueDate = loan.DueDate;

            var today = DateTime.Now.Date;
            var due = loan.DueDate.Date;

            if (today > due)
            {
                TimeSpan diff = today - due;
                OverdueDays = diff.Days;
            }
            else
            {
                OverdueDays = 0;
            }

            CalculateFine();
        }

        private void CalculateFine()
        {
            decimal totalFine = 0;
            string reason = "";

            if (OverdueDays > 0)
            {
                decimal lateFee = OverdueDays * PHAT_QUA_HAN_MOI_NGAY;
                totalFine += lateFee;
                reason += $"Trễ {OverdueDays} ngày ({lateFee:N0}đ). ";
            }

            // 2. Phạt hỏng/mất
            if (SelectedStatus == BookReturnStatus.Lost)
            {
                totalFine += PHAT_MAT_SACH;
                reason += $"Làm mất sách ({PHAT_MAT_SACH:N0}đ).";
            }
            else if (SelectedStatus == BookReturnStatus.Broken)
            {
                totalFine += PHAT_HONG_SACH;
                reason += $"Làm hỏng sách ({PHAT_HONG_SACH:N0}đ).";
            }

            FineAmount = totalFine;
            FineReason = string.IsNullOrEmpty(reason) ? "Không có" : reason;
        }

        [RelayCommand]
        private async Task SaveStatus()
        {
            if (_currentLoan == null) return;

            _currentLoan.ReturnDate = DateTime.Now;
            _currentLoan.LoanStatus = "1"; 

            string newCopyStatus = "1";
            if (SelectedStatus == BookReturnStatus.Broken) newCopyStatus = "-2";
            if (SelectedStatus == BookReturnStatus.Lost) newCopyStatus = "-1";

            _currentLoan.BookCopy.Status = newCopyStatus;

            try
            {
                await _loanService.ReturnBookTransactionAsync(_currentLoan, newCopyStatus);
                WeakReferenceMessenger.Default.Send(new LoanDeletedMessage(_currentLoan));
                MessageBox.Show($"Trả sách thành công!\nTổng phạt: {FineAmount:N0} đ", "Hoàn tất");
                ClosePopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        [RelayCommand]
        private void ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
    public class LoanDeletedMessage
    {
        public Loans DeletedLoan { get; }
        public LoanDeletedMessage(Loans loan) => DeletedLoan = loan;
    }
}

