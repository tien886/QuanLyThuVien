
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.ViewModels;
using QuanLyThuVien.ViewModels.MuonTraSach;
using QuanLyThuVien.ViewModels.QuanLySach;
using QuanLyThuVien.ViewModels.QuanLySachPopup;
using QuanLyThuVien.ViewModels.QuanLySinhVien;
using QuanLyThuVien.Views;
using QuanLyThuVien.Views.MuonTraSachPopup;
using QuanLyThuVien.Views.QuanLySachPopup;
using QuanLyThuVien.Views.QuanLySinhVienPopup;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace QuanLyThuVien.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        // Thuộc tính giữ nội dung của Popup (Ví dụ: StudentItemViewModel)
        [ObservableProperty]
        private object? _currentDialogViewModel;

        // Thuộc tính bật/tắt Popup
        [ObservableProperty]
        private bool _isDialogOpen;

        [RelayCommand]
        private void CloseDialog()
        {
            IsDialogOpen = false;
            CurrentDialogViewModel = null;
        }

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ShowDashBoardView = new ViewModelCommand(ExecuteShowDashBoardView);
            ShowQuanLySinhVienView = new ViewModelCommand(ExecuteShowQuanLySinhVienView);
            ShowQuanLySachView = new ViewModelCommand(ExecuteShowQuanLySachView);
            ShowMuonTraSachView = new ViewModelCommand(ExecuteShowMuonTraSachView);
            ShowAddBookHeadView = new ViewModelCommand(ExecuteShowAddBookHeadView);
            ShowAddPhieuMuonView = new ViewModelCommand(ExecuteShowAddPhieuMuonView);
            ShowAddStudentView = new ViewModelCommand(ExecuteShowAddStudentView);
            ExecuteShowDashBoardView(null);

            // Đăng ký lắng nghe tin nhắn mở Popup từ bất kỳ đâu
            WeakReferenceMessenger.Default.Register<OpenDialogMessage>(this, (r, m) =>
            {
                if (m.ViewModel == null)
                {
                    IsDialogOpen = false;
                    CurrentDialogViewModel = null;
                }
                else
                {
                    CurrentDialogViewModel = m.ViewModel;
                    IsDialogOpen = true;
                }
            });
        }

        private FrameworkElement _currentChildView;
        private string _Title;
        private string _caption;
        private string _buttonName = "";
        private bool _isAddBookHeadViewVisible = true;
        public bool IsAddBookHeadViewVisible
        {
            get => _isAddBookHeadViewVisible;
            set
            {
                if (_isAddBookHeadViewVisible != value)
                {
                    _isAddBookHeadViewVisible = value;
                    OnPropertyChanged(); // notifies XAML bindings
                }
            }
        }
        private bool _isAddStudentViewVisible = true;
        public bool IsAddStudentViewVisible
        {
            get => _isAddStudentViewVisible;
            set
            {
                if (_isAddStudentViewVisible != value)
                {
                    _isAddStudentViewVisible = value;
                    OnPropertyChanged(); // notifies XAML bindings
                }
            }
        }
        private bool _isAddPhieuMuonViewVisible = true;
        public bool IsAddPhieuMuonViewVisible
        {
            get => _isAddPhieuMuonViewVisible;
            set
            {
                if (_isAddPhieuMuonViewVisible != value)
                {
                    _isAddPhieuMuonViewVisible = value;
                    OnPropertyChanged(); // notifies XAML bindings
                }
            }
        }


        public FrameworkElement CurrentChildView { get => _currentChildView; set { _currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); } }
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(nameof(Title)); } }
        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public string ButtonName { get => _buttonName; set { _buttonName = value; OnPropertyChanged(nameof(ButtonName)); } }
        // ICommand
        public ICommand ShowDashBoardView { get; }
        public ICommand ShowQuanLySinhVienView { get; }
        public ICommand ShowQuanLySachView { get; }
        public ICommand ShowMuonTraSachView { get; }
        public ICommand ShowAddStudentView { get; }
        public ICommand ShowAddBookHeadView { get; }
        public ICommand ShowAddPhieuMuonView { get; }
        private void ExecuteShowMuonTraSachView(object obj)
        {
            IsAddStudentViewVisible = false;
            IsAddPhieuMuonViewVisible = true;
            IsAddBookHeadViewVisible = false;
            CurrentChildView = _serviceProvider.GetRequiredService<MuonTraSachView>();
            Title = "Mượn trả sách";
            Caption = "Quản lý các giao dịch mượn và trả sách";
            ButtonName = "Thêm phiếu mượn";
        }

        private void ExecuteShowQuanLySachView(object obj)
        {
            IsAddStudentViewVisible = false;
            IsAddPhieuMuonViewVisible = false;
            IsAddBookHeadViewVisible = true;
            CurrentChildView = _serviceProvider.GetRequiredService<QuanLySachView>();
            Title = "Quản Lý Sách";
            Caption = "Quản lý thông tin đầu sách và bản sao trong thư viện";
            ButtonName = "Thêm đầu sách mới";
        }

        private void ExecuteShowQuanLySinhVienView(object obj)
        {
            IsAddStudentViewVisible = true;
            IsAddPhieuMuonViewVisible = false;
            IsAddBookHeadViewVisible = false;
            CurrentChildView = _serviceProvider.GetRequiredService<QuanLySinhVienView>();
            Title = "Quản Lý Sinh Viên";
            Caption = "Quản lý thông tin và tài khoản sinh viên";
            ButtonName = "Tạo tài khoản mới";
        }

        private void ExecuteShowDashBoardView(object obj)
        {
            IsAddStudentViewVisible = false;
            IsAddPhieuMuonViewVisible = false;
            IsAddBookHeadViewVisible = false;
            CurrentChildView = _serviceProvider.GetRequiredService<DashBoardView>();
            Title = "Dashboard";
            Caption = "Tổng quan hệ thống quản lý thư viện";
        }
        private void ExecuteShowAddBookHeadView(object obj)
        {
            Debug.WriteLine("Show add bookhead");
            var themBooKHeadPopup = _serviceProvider.GetRequiredService<ThemBooKHeadPopup>();
            var vm = _serviceProvider.GetRequiredService<ThemBookHeadViewModel>();
            IsDialogOpen = true;
            CurrentDialogViewModel = vm;
        }
        private void ExecuteShowAddStudentView(object obj)
        {
            Debug.WriteLine("Show add student");
            var themPhieuMuonPopup = _serviceProvider.GetRequiredService<ThemSinhVienPopup>();
            var vm = _serviceProvider.GetRequiredService<ThemSinhVienViewModel>();
            IsDialogOpen = true;
            CurrentDialogViewModel = vm;
        }

        private void ExecuteShowAddPhieuMuonView(object obj)
        {
            Debug.WriteLine("Show add phieu muon");
            var themPhieuMuonPopup = _serviceProvider.GetRequiredService<ThemPhieuMuonPopup>();
            var vm = _serviceProvider.GetRequiredService<ThemPhieuMuonViewModel>();
            IsDialogOpen = true;
            CurrentDialogViewModel = vm;
        }

    }

    public class OpenDialogMessage
    {
        public object ViewModel { get; }
        public OpenDialogMessage(object vm) => ViewModel = vm;
    }
}
