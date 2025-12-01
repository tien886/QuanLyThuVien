using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public class BookCopyRepository : IBookCopyService
    {
        private readonly DataContext _dataContext;
        public BookCopyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<BookCopies>> GetAllBookCopiesAsync(Books book)
        {
            return await _dataContext.BookCopies.
                Where(bc => bc.BookID == book.BookID && (bc.Status == "1" || bc.Status == "0" || bc.Status == "-1" || bc.Status == "-2"))
                .Include(bc => bc.Location) 
                .ToListAsync();
        }
        public async Task<int> GetTotalBookCopiesAsync()
        {
            return await _dataContext.BookCopies.CountAsync();
        }
        public async Task<int> AddBookCopiesAsync(BookCopies bookCopies)
        {
            await _dataContext.BookCopies.AddAsync(bookCopies);
            return await _dataContext.SaveChangesAsync();
        }
        public async Task<string> GetNextAvailableBookCopyID()
        {
            var maxIdString = await _dataContext.BookCopies
                .OrderByDescending(bc => bc.CopyID)
                .Select(bc => bc.CopyID)
                .FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(maxIdString))
                return "CP000001";

            if (!maxIdString.StartsWith("CP"))
                return "CP000001";

            string numericPart = maxIdString.Substring(2);

            if (!int.TryParse(numericPart, out int number))
                number = 0;   // fallback if parsing fails

            number++;

            return "CP" + number.ToString("D6");
        }

        //public async Task<ISeries[]> GetBookStatusData()
        //{
        //    int coSan = _dataContext.BookCopies.Count(bc => bc.Status == "1");
        //    int dangMuon = _dataContext.BookCopies.Count(bc => bc.Status == "0");
        //    int mat = _dataContext.BookCopies.Count(bc => bc.Status == "-1");
        //    int hong = _dataContext.BookCopies.Count(bc => bc.Status == "-2");

        //    var PieChartSeries = new ISeries[]
        //    {
        //        new PieSeries<int>
        //        {
        //            Values = new [] { coSan },
        //            Name = "Có sẵn",
        //            InnerRadius = 50,
        //            Fill = new SolidColorPaint(SKColor.Parse("#10B981")) // Màu Success/1
        //        },
        //        new PieSeries<int>
        //        {
        //            Values = new [] { dangMuon },
        //            Name = "Đang mượn",
        //            InnerRadius = 50,
        //            Fill = new SolidColorPaint(SKColor.Parse("#0EA5E9")) // Màu InUse
        //        },
        //        new PieSeries<int>
        //        {
        //            Values = new [] { hong },
        //            Name = "Hỏng",
        //            InnerRadius = 50,
        //            Fill = new SolidColorPaint(SKColor.Parse("#F59E0B")) // Màu Warning/Waiting
        //        },
        //        new PieSeries<int>
        //        {
        //            Values = new [] { mat },
        //            Name = "Mất",
        //            InnerRadius = 50,
        //            Fill = new SolidColorPaint(SKColor.Parse("#EF4444")) // Màu Danger/Overdue
        //        }
        //    }; ;


        //    return PieChartSeries;
        //}
        public async Task<BookStatusStats> GetBookStatusStatsAsync()
        {
            // Logic truy vấn giữ nguyên, nhưng trả về DTO
            int coSan = await _dataContext.BookCopies.CountAsync(bc => bc.Status == "1");
            int dangMuon = await _dataContext.BookCopies.CountAsync(bc => bc.Status == "0");
            int mat = await _dataContext.BookCopies.CountAsync(bc => bc.Status == "-1");
            int hong = await _dataContext.BookCopies.CountAsync(bc => bc.Status == "-2");

            return new BookStatusStats
            {
                CoSan = coSan,
                DangMuon = dangMuon,
                Mat = mat,
                Hong = hong
            };
        }
    }
}
