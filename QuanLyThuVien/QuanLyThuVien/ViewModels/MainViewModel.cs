
using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.ViewModels;
using QuanLyThuVien.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace QuanLyThuVien.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ShowDashBoardView = new ViewModelCommand(ExecuteShowDashBoardView);
            ShowQuanLySinhVienView = new ViewModelCommand(ExecuteShowQuanLySinhVienView);
            ShowQuanLySachView = new ViewModelCommand(ExecuteShowQuanLySachView);
            ShowMuonTraSachView = new ViewModelCommand(ExecuteShowMuonTraSachView); 
            ExecuteShowDashBoardView(null);
        }

        private FrameworkElement _currentChildView;
        private string _Title;
        private string _caption;
        private string _buttonName = "";
        private bool _isAddButtonVisible = true;
        public bool IsAddButtonVisible
        {
            get => _isAddButtonVisible;
            set
            {
                if (_isAddButtonVisible != value)
                {
                    _isAddButtonVisible = value;
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
        private void ExecuteShowMuonTraSachView(object obj)
        {
            IsAddButtonVisible = true;
            CurrentChildView = _serviceProvider.GetRequiredService<MuonTraSachView>();
            Title = "Mượn trả sách";
            Caption = "Quản lý các giao dịch mượn và trả sách";
            ButtonName = "Thêm phiếu mượn";
        }

        private void ExecuteShowQuanLySachView(object obj)
        {
            IsAddButtonVisible = true;
            CurrentChildView = _serviceProvider.GetRequiredService<QuanLySachView>();
            Title = "Quản Lý Sách";
            Caption = "Quản lý thông tin đầu sách và bản sao trong thư viện";
            ButtonName = "Thêm đầu sách mới";
        }

        private void ExecuteShowQuanLySinhVienView(object obj)
        {
            IsAddButtonVisible = true;
            CurrentChildView = _serviceProvider.GetRequiredService<QuanLySinhVienView>();
            Title = "Quản Lý Sinh Viên";
            Caption = "Quản lý thông tin và tài khoản sinh viên";
            ButtonName = "Tạo tài khoản mới";
        }

        private void ExecuteShowDashBoardView(object obj)
        {
            IsAddButtonVisible = false;
            CurrentChildView = _serviceProvider.GetRequiredService<DashBoardView>();
            Title = "Dashboard";
            Caption = "Tổng quan hệ thống quản lý thư viện";
        }
    }
}
