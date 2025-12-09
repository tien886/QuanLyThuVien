using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Repositories
{
    public class LoanRepository : ILoanService
    {
        private readonly DataContext _dataContext;
        public LoanRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        // Cho quản lý mượn trả
        public async Task<IEnumerable<Loans>> GetAllLoansAsync()
        {
            return await _dataContext.Loans
                .Include(s => s.Student)
                .Include(b => b.BookCopy.Book)
                .Where(l => l.LoanStatus == "0")
                .OrderByDescending(l => l.ReturnDate == null && l.DueDate < DateTime.Now)  // overdue first
                .ThenBy(l => l.DueDate)  // optional, sort overdue by oldest due-date
                .ToListAsync();
        }
        

        // Thêm, sửa phiếu mượn
        public async Task AddLoan(Loans loan)
        {
            _dataContext.Loans.Add(loan);
            await _dataContext.SaveChangesAsync();
        }
        public async Task UpdateLoan(Loans loan)
        {
            _dataContext.Loans.Update(loan);
            await _dataContext.SaveChangesAsync();
        }


        // Lấy số liệu tổng quát cho các badges (Dashboard)
        public async Task<int> GetCurrentlyBorrowedBooksAsync()
        {
            return await _dataContext.Loans
                .Where(l => l.LoanStatus == "0")
                .Select(l => l.BookCopy)
                .Distinct()
                .CountAsync();
        }
        public async Task<int> GetCurrentBorrowingStudentsAsync()
        {
            return await _dataContext.Loans
                .Where(l => l.LoanStatus == "0")
                .Select(l => l.StudentID)
                .Distinct()
                .CountAsync();
        }
        public async Task<int> GetOverdueBooksAsyncCount()
        {
            return await _dataContext.Loans
                .Where(l => l.LoanStatus == "0" && l.DueDate < DateTime.Now.Date)
                .CountAsync();
        }
        public async Task<int> GetDaTraTheoThang(DateTime present)
        {
            int month = present.Month;
            int year = present.Year;

            return await _dataContext.Loans
                .CountAsync(l =>
                    l.LoanStatus == "1" && l.ReturnDate != null
                     && l.ReturnDate.Value.Month == month
                     && l.ReturnDate.Value.Year == year
                );
        }


        // Lấy data cho thống kê phiếu mượn theo sinh viên (Quản lý sinh viên)
        public async Task<StudentLoanStats> GetLoanStatsByStudentIdAsync(int studentId)
        {
            // Lấy tất cả phiếu mượn của sinh viên này (Query 1 lần)
            var studentLoans = await _dataContext.Loans
                                    .Where(l => l.StudentID == studentId)
                                    .AsNoTracking()
                                    .ToListAsync();

            // Cái này query trên studentLoans nên không phát sinh thêm truy vấn xuống database nào nữa
            return new StudentLoanStats
            {
                // 1. Tổng đã mượn (Tính cả lịch sử)
                TotalBorrowed = studentLoans.Count, 

                // 2. Đang mượn (Status = "0" là đang mượn - dựa theo logic cũ của bạn)
                CurrentlyBorrowed = studentLoans.Count(l => l.LoanStatus == "0"),

                // 3. Quá hạn (Đang mượn VÀ Quá hạn trả)
                Overdue = studentLoans.Count(l => l.LoanStatus == "0" && DateTime.Now > l.DueDate)
            };
        }
        //public async Task<IEnumerable<LoanTrendStats>> GetLoanTrendsAsync()
        //{
        //    var result = new List<LoanTrendStats>();
        //    var numOfMonths = 6;

        //    // Duyệt qua 6 tháng gần nhất (từ 5 tháng trước đến tháng hiện tại)
        //    for (int i = numOfMonths - 1; i >= 0; i--)
        //    {
        //        var date = DateTime.Now.AddMonths(-i);
        //        var month = date.Month;
        //        var year = date.Year;

        //        // 1. Đếm số lượng MƯỢN trong tháng này
        //        int borrowed = await _dataContext.Loans
        //            .AsNoTracking()
        //            .CountAsync(l => l.LoanDate != null &&
        //                             l.LoanDate.Month == month &&
        //                             l.LoanDate.Year == year);

        //        // 2. Đếm số lượng TRẢ trong tháng này
        //        int returned = await _dataContext.Loans
        //            .AsNoTracking()
        //            .CountAsync(l => l.LoanStatus == "1" && 
        //                                l.ReturnDate != null && 
        //                                l.ReturnDate.Value.Month == month && 
        //                                l.ReturnDate.Value.Year == year);

        //        result.Add(new LoanTrendStats
        //        {
        //            Month = month,
        //            Year = year,
        //            BorrowedCount = borrowed,
        //            ReturnedCount = returned
        //        });
        //    }

        //    return result;
        //}
        public async Task<IEnumerable<LoanTrendStats>> GetLoanTrendsAsync(DateTime startDate, DateTime endDate)
        {
            // Lọc dữ liệu trong khoảng thời gian
            var query = await _dataContext.Loans
                .AsNoTracking()
                .Where(l => (l.LoanDate >= startDate && l.LoanDate < endDate) ||
                            (l.ReturnDate >= startDate && l.ReturnDate < endDate))
                .ToListAsync();

            // Nhóm theo tháng/năm
            var result = query
                .GroupBy(l => new
                {
                    Month = l.LoanDate.Month != 0 ? l.LoanDate.Month : l.ReturnDate.Value.Month,
                    Year = l.LoanDate.Year != 0 ? l.LoanDate.Year : l.ReturnDate.Value.Year
                })
                .Select(g => new LoanTrendStats
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    BorrowedCount = g.Count(x => x.LoanDate >= startDate && x.LoanDate < endDate),
                    ReturnedCount = g.Count(x => x.LoanStatus == "1" && x.ReturnDate >= startDate && x.ReturnDate < endDate)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();

            return result;
        }


        // Lấy data cho thống kê thể loại sách phổ biến (Dashboard)
        public async Task<IEnumerable<CategoryLoanStats>> GetLoanStatsByCategoryAsync(int top)
        {
            // Logic: Group by Tên thể loại và đếm số lượng phiếu mượn
            var stats = await _dataContext.Loans
                .Include(l => l.BookCopy)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.BookCategory)
                .Where(l => l.BookCopy.Book.BookCategory != null) // Đảm bảo không null
                .GroupBy(l => l.BookCopy.Book.BookCategory.CategoryName)
                .Select(g => new CategoryLoanStats
                {
                    CategoryName = g.Key,
                    LoanCount = g.Count()
                })
                .OrderByDescending(x => x.LoanCount) // Sắp xếp từ cao xuống thấp cho đẹp
                .Take(top) // Chỉ lấy top 7 thể loại phổ biến nhất
                .AsNoTracking()
                .ToListAsync();

            return stats;
        }


        // Lấy data cho thống kê sách được mượn nhiều nhất (Dashboard)
        public async Task<IEnumerable<BookLoanStats>> GetTopBorrowedBooksAsync(int top)
        {
            return await _dataContext.Loans
                .Include(l => l.BookCopy)
                    .ThenInclude(bc => bc.Book)
                .Where(l => l.BookCopy != null && l.BookCopy.Book != null)
                .GroupBy(l => l.BookCopy.Book.Title)
                .Select(g => new BookLoanStats
                {
                    Title = g.Key,
                    LoanCount = g.Count()
                })
                .OrderByDescending(x => x.LoanCount)
                .Take(top) 
                .AsNoTracking()
                .ToListAsync();
        }


        // Lấy data cho thống kê phiếu mượn gần đây (Dashboard)
        public async Task<IEnumerable<Loans>> GetRecentLoansAsync(int count)
        {
            return await _dataContext.Loans
                .Include(l => l.Student)
                .Include(l => l.BookCopy.Book)
                .OrderByDescending(l => l.LoanDate) // Sắp xếp giảm dần theo ngày mượn
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }


        // Lấy data cho thống kê sách quá hạn (Dashboard)
        public async Task<IEnumerable<OverdueBookStats>> GetOverdueBooksAsync(int count)
        {
            var today = DateTime.Now.Date;

            return await _dataContext.Loans
                .Include(l => l.Student)
                .Include(l => l.BookCopy.Book)
                .Where(l => l.LoanStatus == "0" && l.DueDate < today)
                .OrderBy(l => l.DueDate) 
                .Take(count)
                .Select(l => new OverdueBookStats
                {
                    BookTitle = l.BookCopy.Book.Title,
                    ReaderName = l.Student.StudentName,
                    DueDate = l.DueDate,
                   
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetTotalPages(int pageSize, string keyword = "")
        {
            var query = _dataContext.Loans.Where(l => l.LoanStatus == "0"); // Chỉ tính các phiếu mượn chưa trả 
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Include(l => l.Student)
                             .Include(l => l.BookCopy.Book)
                             .Where(l => l.Student.StudentName.Contains(keyword) ||
                                         l.BookCopy.Book.Title.Contains(keyword) ||
                                         l.LoanID.ToString().Contains(keyword));
            }
            int totalCount = await query.CountAsync();
            return (int)Math.Ceiling((double)totalCount / pageSize);
        }
        public async Task<IEnumerable<Loans>> GetLoansPage(int pageIndex, int pageSize, string keyword = "")
        {
            var query = _dataContext.Loans
                .Include(l => l.Student)
                .Include(l => l.BookCopy).ThenInclude(bc => bc.Book)
                .Where(l => l.LoanStatus == "0") // Chỉ lấy các phiếu mượn chưa trả
                .OrderByDescending(l => l.LoanDate) // Mới nhất lên đầu
                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(l => l.Student.StudentName.Contains(keyword) ||
                                         l.BookCopy.Book.Title.Contains(keyword) ||
                                         l.LoanID.ToString().Contains(keyword));
            }

            return await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
        }


        // Nhận trả sách
        public async Task ReturnBookTransactionAsync(Loans loan, string newCopyStatus)
        {
            // 1. Bắt đầu Transaction (Giao dịch)
            using var transaction = await _dataContext.Database.BeginTransactionAsync();

            try
            {
                // A. CẬP NHẬT BẢNG LOANS
                // Đảm bảo EF Core theo dõi đối tượng này
                _dataContext.Loans.Update(loan);

                // B. CẬP NHẬT BẢNG BOOKCOPIES
                // Tìm bản sao sách tương ứng trong DB để đảm bảo chắc chắn
                var bookCopy = await _dataContext.BookCopies.FirstOrDefaultAsync(c => c.CopyID == loan.CopyID);

                if (bookCopy != null)
                {
                    bookCopy.Status = newCopyStatus;
                    _dataContext.BookCopies.Update(bookCopy);
                }

                // C. LƯU THAY ĐỔI
                await _dataContext.SaveChangesAsync();

                // D. CAM KẾT (Hoàn tất giao dịch)
                // Chỉ khi code chạy đến đây mà không lỗi thì dữ liệu mới thực sự được lưu
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // E. NẾU CÓ LỖI -> HOÀN TÁC TẤT CẢ
                await transaction.RollbackAsync();
                throw; // Ném lỗi ra ngoài để ViewModel hiển thị MessageBox
            }
        }
    }
}

