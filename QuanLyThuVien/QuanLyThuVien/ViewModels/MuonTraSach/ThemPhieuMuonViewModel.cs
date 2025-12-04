using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Diagnostics;

namespace QuanLyThuVien.ViewModels.MuonTraSach
{
    public partial class ThemPhieuMuonViewModel : ObservableObject
    {
        private readonly IStudentService _studentService;
        private readonly IBookCopyService _bookCopyService;
        private readonly ILoanService _loanService;
        public ThemPhieuMuonViewModel(
            IStudentService studentService,
            IBookCopyService bookCopyService,
            ILoanService loanService
            )
        {
            _studentService = studentService;
            _bookCopyService = bookCopyService;
            _loanService = loanService;
        }
        [ObservableProperty]
        private int? studentId;
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
        partial void OnStudentIdChanged(int? value)
        {
            if (value.HasValue)
                _ = LoadStudentAsync(value.Value);
            else
                StudentName = string.Empty;
        }

        // When BookCopyCode changes, try to load book copy
        partial void OnBookCopyIDChanged(string? value)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length < 9)
                _ = LoadBookCopyAsync(value);
            else
                BookTitle = string.Empty;
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
