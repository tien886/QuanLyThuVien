using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QuanLyThuVien.ViewModels.MuonTraSach
{
    public partial class ThemPhieuMuonViewModel : ObservableObject
    {
        private readonly IStudentService _studentService;
        private readonly IBookCopyService _bookCopyService;
        private readonly IBookCategoryService _bookCategoryService;
        private readonly ILoanService _loanService;
        private readonly IGenreService _genreService;
        public ThemPhieuMuonViewModel(
            IStudentService studentService,
            IBookCopyService bookCopyService,
            ILoanService loanService,
            IGenreService genreService,
            IBookCategoryService bookCategoryService
            )
        {
            _studentService = studentService;
            _bookCopyService = bookCopyService;
            _loanService = loanService;
            _genreService = genreService;
            _bookCategoryService = bookCategoryService;
            LoadPage();
        }
        [ObservableProperty]
        private int? studentId;
        [ObservableProperty]
        private string? categoryName;
        [ObservableProperty]
        private string? genreName;
        [ObservableProperty]
        private string? studentName;
        [ObservableProperty]
        private string? bookCopyID;
        [ObservableProperty]
        private string? bookTitle;
        [ObservableProperty]
        private DateTime? loandate;
        [ObservableProperty]
        private DateTime? duedate;
        

        [RelayCommand]
        public async Task CreateLoan()
        {
            Loans loans = new Loans
            {
                LoanID = await _loanService.GetNextAvailableLoanID(),
                StudentID = StudentId,
                CopyID = BookCopyID,
                LoanStatus = "0",
                LoanDate = Loandate,
                DueDate = Duedate,
                ReturnDate = null,
            };
            await _loanService.AddLoan(loans);
        }
        private void LoadPage()
        {
            Loandate = DateTime.Now;
            bookCopyID = "CP";
        }
        partial void OnStudentIdChanged(int? value)
        {
            if (value.HasValue)
                _ = LoadStudentAsync(value.Value);
            else
                StudentName = string.Empty;
        }
        partial void OnBookCopyIDChanged(string? value)
        {
            _ = OnBookCopyIDChangedAsync(value);
        }
        private async Task OnBookCopyIDChangedAsync(string? value)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length == 8)
            {
                await LoadBookCopyAsync(value);
                var copy = await _bookCopyService.GetBookCopiesByIDAsync(value);
                if (copy == null)
                    return;
                var genre = await _genreService.GetGenreByID(copy.Book.GenreID);
                int loanDays = genre.LoanDurationDays;
                GenreName = genre.GenreName;
                var category = await _bookCategoryService.GetBookCategoryByID(copy.Book.CategoryID);
                CategoryName = category.CategoryName;
                if (loanDays != 0) 
                    Duedate = DateTime.Now.AddDays(loanDays);
            }
        }
        private async Task LoadStudentAsync(int id)
        {
            var student = await _studentService.GetStudentByIDAsync(id);
            StudentName = student?.StudentName ?? "Không tìm thấy";
        }

        private async Task LoadBookCopyAsync(string copyID)
        {
            var copy = await _bookCopyService.GetBookCopiesByIDAsync(copyID);
            BookTitle = copy?.Book?.Title ?? "Không tìm thấy";
            Debug.WriteLine(BookTitle);
        }
        
    }
}
