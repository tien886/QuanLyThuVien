using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.Views;
using System.Windows;
using System.Windows.Input;
namespace QuanLyThuVien.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        // Tiêu đề của trang 
        [ObservableProperty] private string _title;
        [ObservableProperty] private string _caption;

        // Chỉ cần 1 bộ thuộc tính cho nút duy nhất
        [ObservableProperty] private string _headerButtonLabel;
        [ObservableProperty] private ICommand _headerButtonCommand;
        [ObservableProperty] private bool _isHeaderButtonVisible;

        [ObservableProperty] private FrameworkElement _currentChildView;

        // Popup logic
        [ObservableProperty] private object? _currentDialogViewModel;
        [ObservableProperty] private bool _isDialogOpen;

        public ICommand ShowDashBoardView { get; }
        public ICommand ShowQuanLySinhVienView { get; }
        public ICommand ShowQuanLySachView { get; }
        public ICommand ShowMuonTraSachView { get; }

        [RelayCommand]
        private void CloseDialog()
        {
            IsDialogOpen = false;
            CurrentDialogViewModel = null;
        }

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            // Khởi tạo các command điều hướng
            ShowDashBoardView = new RelayCommand(ExecuteShowDashBoardView);
            ShowQuanLySinhVienView = new RelayCommand(ExecuteShowQuanLySinhVienView);
            ShowQuanLySachView = new RelayCommand(ExecuteShowQuanLySachView);
            ShowMuonTraSachView = new RelayCommand(ExecuteShowMuonTraSachView);

            // Mặc định vào Dashboard
            ExecuteShowDashBoardView();

            // Đăng ký nhận tin nhắn mở Popup 
            WeakReferenceMessenger.Default.Register<OpenDialogMessage>(this, (r, message) =>
            {
                if (message.ViewModel == null)
                {
                    CloseDialog();
                }
                else
                {
                    CurrentDialogViewModel = message.ViewModel;
                    IsDialogOpen = true;
                }
            });
        }

        // Hàm để thay đổi nút thêm (sinh viên, sách, phiếu mượn) - Hàm này sẽ được gọi mỗi khi chuyển trang
        private void UpdateHeaderState(string title, string caption, FrameworkElement view)
        {
            Title = title;
            Caption = caption;
            CurrentChildView = view;

            // Cập nhật trạng thái nút header dựa trên ViewModel của View con hiện tại
            if (view.DataContext is IHeaderActionViewModel actionVM && actionVM.IsHeaderButtonVisible)
            {
                // Trường hợp có nút, lấy thông tin từ ViewModel con hiện lên cha 
                HeaderButtonLabel = actionVM.HeaderButtonLabel;
                HeaderButtonCommand = actionVM.HeaderButtonCommand;
                IsHeaderButtonVisible = true;
            }
            else
            {
                // Trường hợp không có nút thì reset null/rỗng
                HeaderButtonLabel = string.Empty; // Xóa chữ
                HeaderButtonCommand = null;       // Gỡ lệnh (tránh bấm nhầm)
                IsHeaderButtonVisible = false;    // Ẩn nút đi
            }
        }

        // --- Hàm điều hướng ---
        private void ExecuteShowDashBoardView()
        {
            var view = _serviceProvider.GetRequiredService<DashBoardView>();
            UpdateHeaderState("Dashboard", "Tổng quan hệ thống quản lý thư viện", view);
        }

        private void ExecuteShowQuanLySinhVienView()
        {
            var view = _serviceProvider.GetRequiredService<QuanLySinhVienView>();
            UpdateHeaderState("Quản Lý Sinh Viên", "Quản lý thông tin và tài khoản sinh viên", view);
        }

        private void ExecuteShowQuanLySachView()
        {
            var view = _serviceProvider.GetRequiredService<QuanLySachView>();
            UpdateHeaderState("Quản Lý Sách", "Quản lý thông tin đầu sách và bản sao", view);
        }

        private void ExecuteShowMuonTraSachView()
        {
            var view = _serviceProvider.GetRequiredService<MuonTraSachView>();
            UpdateHeaderState("Mượn trả sách", "Quản lý các giao dịch mượn và trả sách", view);
        }
    }

    public class OpenDialogMessage
    {
        public object ViewModel { get; }
        public OpenDialogMessage(object vm) => ViewModel = vm;
    }
}

