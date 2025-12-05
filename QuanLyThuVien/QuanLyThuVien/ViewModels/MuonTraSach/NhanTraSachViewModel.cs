using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyThuVien.ViewModels.MuonTraSach
{
    public partial class NhanTraSachViewModel : ObservableObject
    {
        private readonly ILoanService _loanService;

        private Loans CurrentLoan;

        public NhanTraSachViewModel(ILoanService loanService)
        {
            _loanService = loanService;
            selectedStatus = BookReturnStatus.OK;
        }

        public enum BookReturnStatus
        {
            OK = 1,   // "1"  : available
            Lost = -1,  // "-1" : lost
            Broken = -2   // "-2" : broken
        }
        public Array StatusOptions => Enum.GetValues(typeof(BookReturnStatus));

        [ObservableProperty]
        private BookReturnStatus selectedStatus;

        // Gán loan khi mở popup
        public void SetCurrentLoan(Loans loan)
        {
            CurrentLoan = loan;
        }

        [RelayCommand]
        private void SaveStatus()
        {
            if (CurrentLoan == null)
                return;
            UpdateLoanAfterReturn(CurrentLoan);
            MessageBox.Show("Trả sách thành công", "Thành công", MessageBoxButton.OK);
            _ = ClosePopup();
        }

        private void UpdateLoanAfterReturn(Loans loan)
        {
            loan.BookCopy.Status = ((int)SelectedStatus).ToString();

            loan.ReturnDate = DateTime.Now;

            loan.LoanStatus = "1";

            _loanService.UpdateLoan(loan); 
        }
        private async Task ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
}
