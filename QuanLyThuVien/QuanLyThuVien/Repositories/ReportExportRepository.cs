using ClosedXML.Excel;
using QuanLyThuVien.Data;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Helper;
using QuanLyThuVien.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public class ReportExportRepository : IReportExportService
    {
        private readonly DataContext _dataContext;
        public ReportExportRepository(DataContext dataContext) { 
            _dataContext = dataContext;
        }
        public async Task ExportCSVAsync(IEnumerable<LoanTrendStats> loanTrendStats,
                                         IEnumerable<CategoryLoanStats> categoryLoanStats,
                                         IEnumerable<MonthlyReaderStats> monthlyReaderStats,
                                         BookStatusStats bookStatusStats)
        {
            int year = DateTime.Now.Year;
            var sb = new StringBuilder();

            // --- Block 1: Summary line for book statuses ---
            sb.AppendLine($"Year: , {year}");
            sb.AppendLine();
            sb.AppendLine("Metric,Value");
            int totalCopies = bookStatusStats.CoSan + bookStatusStats.DangMuon +
                              bookStatusStats.Hong + bookStatusStats.Mat;
            sb.AppendLine($"Tổng bản sao,{totalCopies}");
            sb.AppendLine($"Có sẵn,{bookStatusStats.CoSan}");
            sb.AppendLine($"Đang mượn,{bookStatusStats.DangMuon}");
            sb.AppendLine($"Hỏng,{bookStatusStats.Hong}");
            sb.AppendLine($"Mất,{bookStatusStats.Mat}");
            sb.AppendLine();

            // --- Block 2: Bảng sinh viên + mượn/trả theo tháng ---
            sb.AppendLine("Metric,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12");

            int getReaders(int m) =>
                monthlyReaderStats.FirstOrDefault(x => x.Year == year && x.Month == m)?.Count ?? 0;

            LoanTrendStats getLoan(int m) =>
                loanTrendStats.FirstOrDefault(x => x.Year == year && x.Month == m)
                ?? new LoanTrendStats { Year = year, Month = m };

            // Row 1: số sinh viên đăng ký đọc
            sb.Append($"Số sinh viên đăng kí đọc");
            for (int m = 1; m <= 12; m++)
                sb.Append(',').Append(getReaders(m));
            sb.AppendLine();

            // Row 2: số sách mượn
            sb.Append($"Số sách mượn");
            for (int m = 1; m <= 12; m++)
                sb.Append(',').Append(getLoan(m).BorrowedCount);
            sb.AppendLine();

            // Row 3: số sách trả
            sb.Append($"Số sách trả");
            for (int m = 1; m <= 12; m++)
                sb.Append(',').Append(getLoan(m).ReturnedCount);
            sb.AppendLine();
            sb.AppendLine();

            // --- Block 3: Bảng thống kê số sách được mượn của thể loại theo tháng ---
            sb.AppendLine("Category,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12");

            var categories = categoryLoanStats
                .Select(c => c.CategoryName)
                .Distinct()
                .OrderBy(c => c);

            foreach (var cat in categories)
            {
                sb.Append($"{cat}");

                for (int m = 1; m <= 12; m++)
                {
                    int count = categoryLoanStats
                        .FirstOrDefault(c => c.Year == year && c.Month == m && c.CategoryName == cat)
                        ?.LoanCount ?? 0;

                    sb.Append(',').Append(count);
                }
                sb.AppendLine();
            }

            var downloadsFolder = KnownFolders.GetDownloadPath();
            var filePath = Path.Combine(downloadsFolder, $"Dashboard_{year}.csv");

            await File.WriteAllTextAsync(filePath, sb.ToString(), Encoding.UTF8);
            
        }
        public async Task ExportExcelAsync(IEnumerable<LoanTrendStats> loanTrendStats,
                                        IEnumerable<CategoryLoanStats> categoryLoanStats,
                                        IEnumerable<MonthlyReaderStats> monthlyReaderStats,
                                        BookStatusStats bookStatusStats)
        {
            int year = DateTime.Now.Year;

            var wb = new XLWorkbook();

            //
            // ===================== SHEET 1 – SUMMARY =====================
            //
            var wsSummary = wb.AddWorksheet("Book_Stats");

            wsSummary.Cell("A1").Value = "Metric";
            wsSummary.Cell("B1").Value = "Value";

            int totalCopies = bookStatusStats.CoSan +
                              bookStatusStats.DangMuon +
                              bookStatusStats.Hong +
                              bookStatusStats.Mat;

            wsSummary.Cell("A2").Value = "Năm báo cáo";
            wsSummary.Cell("B2").Value = year;

            wsSummary.Cell("A3").Value = "Tổng bản sao";
            wsSummary.Cell("B3").Value = totalCopies;

            wsSummary.Cell("A4").Value = "Có sẵn";
            wsSummary.Cell("B4").Value = bookStatusStats.CoSan;

            wsSummary.Cell("A5").Value = "Đang mượn";
            wsSummary.Cell("B5").Value = bookStatusStats.DangMuon;

            wsSummary.Cell("A6").Value = "Hỏng";
            wsSummary.Cell("B6").Value = bookStatusStats.Hong;

            wsSummary.Cell("A7").Value = "Mất";
            wsSummary.Cell("B7").Value = bookStatusStats.Mat;

            wsSummary.Columns().AdjustToContents();

            //
            // ===================== SHEET 2 – READERS + LOANS =====================
            //
            var wsMonthly = wb.AddWorksheet("Readers_Loans");

            // Header
            wsMonthly.Cell("A1").Value = "Metric";
            wsMonthly.Cell("B1").Value = "T1";
            wsMonthly.Cell("C1").Value = "T2";
            wsMonthly.Cell("D1").Value = "T3";
            wsMonthly.Cell("E1").Value = "T4";
            wsMonthly.Cell("F1").Value = "T5";
            wsMonthly.Cell("G1").Value = "T6";
            wsMonthly.Cell("H1").Value = "T7";
            wsMonthly.Cell("I1").Value = "T8";
            wsMonthly.Cell("J1").Value = "T9";
            wsMonthly.Cell("K1").Value = "T10";
            wsMonthly.Cell("L1").Value = "T11";
            wsMonthly.Cell("M1").Value = "T12";

            int GetReaders(int m) =>
                monthlyReaderStats.FirstOrDefault(x => x.Year == year && x.Month == m)?.Count ?? 0;

            LoanTrendStats GetLoanStat(int m) =>
                loanTrendStats.FirstOrDefault(x => x.Year == year && x.Month == m)
                ?? new LoanTrendStats { Year = year, Month = m };

            // Row 2 – Readers
            wsMonthly.Cell("A2").Value = "Số sinh viên đăng kí đọc";
            for (int m = 1; m <= 12; m++)
                wsMonthly.Cell(2, m + 1).Value = GetReaders(m);

            // Row 3 – Borrowed
            wsMonthly.Cell("A3").Value = "Số sách mượn";
            for (int m = 1; m <= 12; m++)
                wsMonthly.Cell(3, m + 1).Value = GetLoanStat(m).BorrowedCount;

            // Row 4 – Returned
            wsMonthly.Cell("A4").Value = "Số sách trả";
            for (int m = 1; m <= 12; m++)
                wsMonthly.Cell(4, m + 1).Value = GetLoanStat(m).ReturnedCount;

            wsMonthly.Columns().AdjustToContents();

            //
            // ===================== SHEET 3 – CATEGORY MONTHLY =====================
            //
            var wsCat = wb.AddWorksheet("Category_Stats");

            wsCat.Cell("A1").Value = "Category";
            wsCat.Cell("B1").Value = "T1";
            wsCat.Cell("C1").Value = "T2";
            wsCat.Cell("D1").Value = "T3";
            wsCat.Cell("E1").Value = "T4";
            wsCat.Cell("F1").Value = "T5";
            wsCat.Cell("G1").Value = "T6";
            wsCat.Cell("H1").Value = "T7";
            wsCat.Cell("I1").Value = "T8";
            wsCat.Cell("J1").Value = "T9";
            wsCat.Cell("K1").Value = "T10";
            wsCat.Cell("L1").Value = "T11";
            wsCat.Cell("M1").Value = "T12";

            var categories = categoryLoanStats
                .Select(c => c.CategoryName)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            int row = 2;
            foreach (var cat in categories)
            {
                wsCat.Cell(row, 1).Value = cat;

                for (int m = 1; m <= 12; m++)
                {
                    int count = categoryLoanStats
                        .FirstOrDefault(c =>
                            c.CategoryName == cat &&
                            c.Year == year &&
                            c.Month == m)
                        ?.LoanCount ?? 0;

                    wsCat.Cell(row, m + 1).Value = count;
                }

                row++;
            }

            wsCat.Columns().AdjustToContents();
            var downloadsFolder = KnownFolders.GetDownloadPath();
            var filePath = Path.Combine(downloadsFolder, $"Dashboard_{year}.xlsx");
            wb.SaveAs(filePath);
            await Task.CompletedTask;
        }
    }
}
