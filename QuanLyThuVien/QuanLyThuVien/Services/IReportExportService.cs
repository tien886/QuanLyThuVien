using QuanLyThuVien.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Services
{
    public interface IReportExportService 
    {
        Task ExportCSVAsync(IEnumerable<LoanTrendStats> loanTrendStats,
                            IEnumerable<CategoryLoanStats> categoryLoanStats,
                            IEnumerable<MonthlyReaderStats> monthlyReaderStats,
                            BookStatusStats bookStatusStats);
        Task ExportExcelAsync(IEnumerable<LoanTrendStats> loanTrendStats,
                                        IEnumerable<CategoryLoanStats> categoryLoanStats,
                                        IEnumerable<MonthlyReaderStats> monthlyReaderStats,
                                        BookStatusStats bookStatusStats);
    }
}
