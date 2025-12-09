using CommunityToolkit.Mvvm.ComponentModel;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.ViewModels.MuonTraSach
{
    public partial class LoanItemViewModel : ObservableObject
    {
        public Loans Loan { get; }

        public LoanItemViewModel(Loans loan)
        {
            Loan = loan;
        }

        // Các property để Binding ra UI (đỡ phải viết Converter phức tạp)
        public int LoanID => Loan.LoanID;
        public string StudentName => Loan.Student?.StudentName ?? "N/A";
        public string BookTitle => Loan.BookCopy?.Book?.Title ?? "N/A";
        public DateTime LoanDate => Loan.LoanDate;
        public DateTime DueDate => Loan.DueDate;

        public string StatusDescription
        {
            get
            {
                // Vì danh sách này chỉ chứa phiếu chưa trả, nên chỉ cần check Quá hạn
                if (DateTime.Now.Date > Loan.DueDate.Date)
                    return "Quá hạn";

                return "Đang mượn";
            }
        }
    }
}