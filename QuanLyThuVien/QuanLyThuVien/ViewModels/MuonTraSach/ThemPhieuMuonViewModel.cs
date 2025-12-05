using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

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
            Loans loan = new Loans
            {
                LoanID = await _loanService.GetNextAvailableLoanID(),
                StudentID = StudentId,
                CopyID = BookCopyID,
                LoanStatus = "0",
                LoanDate = Loandate,
                DueDate = Duedate,
                ReturnDate = null,
            };
            var copy = await _bookCopyService.GetBookCopiesByIDAsync(BookCopyID);
            copy.Status = "0";
            if (!ValidateLoan(loan))
            {
                MessageBox.Show("Phiếu mượn bị lỗi!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            await _loanService.AddLoan(loan);
            MessageBox.Show("Thêm phiếu mượn thành công!", "Thông báo");
            await ClosePopup();
        }
        private void LoadPage()
        {
            Loandate = DateTime.Now;
            bookCopyID = "CP";
        }
        partial void OnStudentIdChanged(int? value)
        {
            if (value != null)
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
                var copy = await _bookCopyService.GetBookCopiesByIDAsync(value);
                if (copy == null || !ValidateStatusBeforeLoan(copy) )
                    return;
                var book = await _bookCopyService.GetBookByCopyIdAsync(value);
                BookTitle = book.Title ?? "Không tìm thấy";
                Debug.WriteLine(BookTitle);
                var genre = await _bookCopyService.GetGenreByCopyIdAsync(value);
                int loanDays = genre.LoanDurationDays;
                GenreName = genre.GenreName;
                var category = await _bookCopyService.GetBookCategoryByCopyIDAsync(value);
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
        private bool ValidateStatusBeforeLoan(BookCopies bc)
        {
            if (bc.Status == "-1")
            {
                BookTitle = "Bản sao này đang bị hỏng";
                return false;
            }
            if (bc.Status == "-2")
            {
                BookTitle = "Bản sao này đang bị hỏng";
                return false;
            }
            if (bc.Status == "0")
            {
                BookTitle = "Bản sao này đang được mượn";
                return false;
            }
            return true;
        }
        [RelayCommand]
        public async Task ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
        private bool ValidateLoan(Loans loan)
        {
            if (loan.DueDate <= loan.LoanDate || loan.DueDate == null|| loan.LoanDate == null || StudentId == null || BookCopyID == null)
            {

                return false;
            }
            return true;
        }
    }
}
