using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Services;
using System.Diagnostics;
using System.Windows;

namespace QuanLyThuVien.ViewModels
{
    public partial class ExportReportViewModel : ObservableObject
    {
        private readonly IReportExportService _reportExportService;
        private readonly IEnumerable<LoanTrendStats> _loantrendstats;
        private readonly IEnumerable<CategoryLoanStats> _categoryloanstats;
        private readonly IEnumerable<MonthlyReaderStats> _monthlyreaderstats;
        private readonly BookStatusStats _bookstatusstats;

        public ExportReportViewModel(
            IReportExportService reportExportService,
            IEnumerable<LoanTrendStats> loanTrendStats,
            IEnumerable<CategoryLoanStats> categoryLoanStats,
            IEnumerable<MonthlyReaderStats> monthlyReaderStats,
            BookStatusStats bookStatusStats
        )
        {
            _reportExportService = reportExportService;
            _loantrendstats = loanTrendStats;
            _categoryloanstats = categoryLoanStats;
            _monthlyreaderstats = monthlyReaderStats;
            _bookstatusstats = bookStatusStats;
        }

        [RelayCommand]
        public async Task ExportToExcel()
        {
            try
            {
                await _reportExportService.ExportExcelAsync(_loantrendstats, _categoryloanstats, _monthlyreaderstats, _bookstatusstats);
                MessageBox.Show("Xuất file thành công", "Thành công", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            ClosePopup();
        }

        [RelayCommand]
        public async Task ExportToCsv()
        {
            try
            {
                await _reportExportService.ExportCSVAsync(_loantrendstats, _categoryloanstats, _monthlyreaderstats, _bookstatusstats);
                MessageBox.Show("Xuất file thành công", "Thành công", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        [RelayCommand]
        private void ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }
}