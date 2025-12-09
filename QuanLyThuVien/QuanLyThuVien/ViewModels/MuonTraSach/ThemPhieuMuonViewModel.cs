using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Windows;

namespace QuanLyThuVien.ViewModels.MuonTraSach
{
    public partial class ThemPhieuMuonViewModel : ObservableObject
    {
        // Các service
        private readonly IStudentService _studentService;
        private readonly IBookCopyService _bookCopyService;
        private readonly ILoanService _loanService;

        // Các trường nhập liệu 

        [ObservableProperty]
        private int? _studentId;

        [ObservableProperty]
        private string _studentName; // ReadOnly

        [ObservableProperty]
        private string _bookCopyID;

        [ObservableProperty]
        private string _bookTitle; // ReadOnly

        [ObservableProperty]
        private string _categoryName; // ReadOnly (Loại sách)

        [ObservableProperty]
        private string _genreName; // ReadOnly (Thể loại)

        [ObservableProperty]
        private DateTime _loanDate;

        [ObservableProperty]
        private DateTime _dueDate;

        // Biến lưu số ngày được phép mượn của cuốn sách hiện tại
        private int _currentLoanDurationDays = 0;

        public ThemPhieuMuonViewModel(
            IStudentService studentService,
            IBookCopyService bookCopyService,
            ILoanService loanService)
        {
            _studentService = studentService;
            _bookCopyService = bookCopyService;
            _loanService = loanService;

            // Mặc định ngày mượn là hôm nay
            LoanDate = DateTime.Now;
        }

        // Logic xử lí 
        // 1. Khi nhập Mã Sinh Viên -> Tự load tên
        async partial void OnStudentIdChanged(int? value)
        {
            if (value.HasValue)
            {
                var s = await _studentService.GetStudentByIDAsync(value.Value);
                StudentName = s != null ? s.StudentName : "Không tìm thấy sinh viên";
            }
            else StudentName = string.Empty;
        }

        // 2. Khi nhập Mã Bản Sao -> Tự load Sách, Thể loại, tính Hạn trả
        async partial void OnBookCopyIDChanged(string value)
      {
            if (!string.IsNullOrEmpty(value) && value.Length >= 4) // Độ dài tối thiểu để bắt đầu tìm
            {
                var copy = await _bookCopyService.GetBookCopiesByIDAsync(value);

                if (copy != null)
                {
                    // Check trạng thái
                    if (copy.Status != "1") // 1 = Có sẵn
                    {
                        BookTitle = "Sách này không khả dụng (Đang mượn/Hỏng)";
                        ClearBookInfo();
                        return;
                    }

                    // Load thông tin sách
                    var book = await _bookCopyService.GetBookByCopyIdAsync(value);
                    BookTitle = book?.Title ?? "Lỗi dữ liệu sách";

                    // Load Thể loại & Loại sách để hiển thị
                    var genre = await _bookCopyService.GetGenreByCopyIdAsync(value);
                    GenreName = genre?.GenreName ?? "";

                    var category = await _bookCopyService.GetBookCategoryByCopyIDAsync(value);
                    CategoryName = category?.CategoryName ?? "";

                    // Cập nhật số ngày được mượn và tính Hạn trả
                    if (genre != null)
                    {
                        _currentLoanDurationDays = genre.LoanDurationDays;
                        UpdateDueDate(); // Tính toán lại ngày trả
                    }
                }
                else
                {
                    BookTitle = "Không tìm thấy bản sao";
                    ClearBookInfo();
                }
            }
            else
            {
                BookTitle = string.Empty;
                ClearBookInfo();
            }
        }

        // 3. Khi chọn Ngày mượn -> Tự động tính lại Hạn trả
        partial void OnLoanDateChanged(DateTime value)
        {
            UpdateDueDate();
        }

        // Hàm helper tính ngày trả
        private void UpdateDueDate()
        {
            if (_currentLoanDurationDays > 0)
            {
                DueDate = LoanDate.AddDays(_currentLoanDurationDays);
            }
            else
            {
                // Nếu chưa có sách hoặc sách không quy định ngày, mặc định +7 ngày (hoặc giữ nguyên tùy logic)
                DueDate = LoanDate.AddDays(7);
            }
        }

        private void ClearBookInfo()
        {
            GenreName = string.Empty;
            CategoryName = string.Empty;
            _currentLoanDurationDays = 0;
        }

        [RelayCommand]
        public async Task CreateLoan()
        {
            if (!Validate()) return;

            // 1. Tạo object Loan
            var newLoan = new Loans
            {
                StudentID = StudentId,
                CopyID = BookCopyID,
                LoanDate = LoanDate,
                DueDate = DueDate,
                LoanStatus = "0", // 0 = Đang mượn
                // LoanID sẽ được tự sinh trong Service hoặc DB
            };

            try
            {
                // 2. Lưu xuống DB (Bảng Loans)
                await _loanService.AddLoan(newLoan);

                // 3. Cập nhật trạng thái bản sao thành "Đang mượn" (0)
                var copy = await _bookCopyService.GetBookCopiesByIDAsync(BookCopyID);
                copy.Status = "0";
                await _bookCopyService.UpdateCopiesAsync(copy);

                // 4. Load lại object đầy đủ (Student, Book) để gửi Message về UI cập nhật bảng
                // (Vì object newLoan hiện tại chỉ có ID, chưa có các object tham chiếu)
                newLoan.Student = await _studentService.GetStudentByIDAsync(StudentId.Value);
                newLoan.BookCopy = copy;
                // Load Book cho BookCopy để hiển thị Tên sách
                newLoan.BookCopy.Book = await _bookCopyService.GetBookByCopyIdAsync(BookCopyID);

                // 5. Gửi tin nhắn cập nhật danh sách
                WeakReferenceMessenger.Default.Send(new LoanAddedMessage(newLoan));

                MessageBox.Show("Tạo phiếu mượn thành công!", "Thông báo");
                ClosePopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private bool Validate()
        {
            if (StudentId == null || string.IsNullOrEmpty(StudentName) || StudentName.Contains("Không tìm thấy"))
            {
                MessageBox.Show("Mã sinh viên không hợp lệ.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(BookCopyID) || string.IsNullOrEmpty(BookTitle) || BookTitle.Contains("Không tìm thấy") || BookTitle.Contains("không khả dụng"))
            {
                MessageBox.Show("Mã bản sao không hợp lệ hoặc sách không có sẵn.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (DueDate <= LoanDate)
            {
                MessageBox.Show("Hạn trả phải sau ngày mượn.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        [RelayCommand]
        private void ClosePopup()
        {
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(null));
        }
    }

    public class LoanAddedMessage
    {
        public Loans NewLoan { get; }
        public LoanAddedMessage(Loans loan) => NewLoan = loan;
    }
}