
using CommunityToolkit.Mvvm.ComponentModel;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace QuanLyThuVien.ViewModels
{
    public partial class MuonTraSachViewModel : ObservableObject
    {
        private IServiceProvider _serviceProvider;
        private ILoanService _loanService;
        public MuonTraSachViewModel(
            IServiceProvider serviceProvider
            ,ILoanService loanService 
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
            DangMuon = await _loanService.GetDangMuon();
            QuaHan = await _loanService.GetQuaHan();
            DaTraThangNay = await _loanService.GetDaTraTheoThang(DateTime.Now);
            LoanList = new ObservableCollection<Loans>(await _loanService.GetAllLoansAsync());
        }
        public async Task SearchPhieuMuonAsync(string hint)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
