using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.ViewModels;
using QuanLyThuVien.Views;
using System;

namespace QuanLyThuVien.DI
{
    public static class AppService
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            
            // Register Views
            services.AddTransient<MainView>();
            services.AddTransient<DashBoardView>();
            services.AddTransient<MuonTraSachView>();
            services.AddTransient<QuanLySachView>();
            services.AddTransient<QuanLySinhVienView>();
            //Register ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<DashBoardViewModel>();
            services.AddTransient<MuonTraSachViewModel>();
            services.AddTransient<QuanLySachViewModel>();
            services.AddTransient<QuanLySinhVienViewModel>();
            return services;
        }
    }
}
