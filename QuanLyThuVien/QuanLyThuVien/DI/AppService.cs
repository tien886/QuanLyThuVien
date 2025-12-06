using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.Config;
using QuanLyThuVien.Data;
using QuanLyThuVien.Repositories;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels;
using QuanLyThuVien.ViewModels.QuanLySach;
using QuanLyThuVien.ViewModels.QuanLySachPopup;
using QuanLyThuVien.Views;
using QuanLyThuVien.Views.QuanLySachPopup;
using QuanLyThuVien.Views.MuonTraSachPopup;
using QuanLyThuVien.ViewModels.MuonTraSach;
using QuanLyThuVien.Views.QuanLySinhVienPopup;

namespace QuanLyThuVien.DI
{
    public static class AppService
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Register Database
            services.AddSingleton<DatabaseConfig>();
            services.AddDbContext<DataContext>((ServiceProvider, options) =>
            {
                var databasePath = DatabaseConfig.GetDefaultDatabasePath();
                options.UseSqlite($"Data Source={databasePath}");
            });
            // Register Views
            services.AddTransient<MainView>();
            services.AddTransient<DashBoardView>();
            services.AddTransient<MuonTraSachView>();
            services.AddTransient<QuanLySachView>();
            services.AddTransient<QuanLySinhVienView>();
            services.AddTransient<BookDetailAndCopyPopup>();
            services.AddTransient<ThemBookCopyPopup>();
            services.AddTransient<ThemBooKHeadPopup>();
            services.AddTransient<SuaBookHeadPopup>();
            services.AddTransient<ThemPhieuMuonPopup>();
            services.AddTransient<ThemSinhVienPopup>();
            services.AddTransient<NhanTraSachPopup>();
            services.AddTransient<SuaBookCopyPopup>();
            //Register ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<DashBoardViewModel>();
            services.AddTransient<MuonTraSachViewModel>();
            services.AddTransient<QuanLySachViewModel>();
            services.AddTransient<QuanLySinhVienViewModel>();
            services.AddTransient<BookDetailAndCopyViewModel>();
            services.AddTransient<ThemBookCopyViewModel>();
            services.AddTransient<ThemBookHeadViewModel>();
            services.AddTransient<SuaBookHeadViewModel>();
            services.AddTransient<ThemPhieuMuonViewModel>();
            services.AddTransient<ThemSinhVienViewModel>();
            services.AddTransient<NhanTraSachViewModel>();
            services.AddTransient<SuaBookCopyViewModel>();
            // Register Services
            services.AddTransient<IBookService, BookRepository>();
            services.AddTransient<IBookCopyService, BookCopyRepository>();
            services.AddTransient<IBookCategoryService, BookCategoryRepository>();
            services.AddTransient<ILoanService, LoanRepository>();
            services.AddTransient<IStudentService, StudentRepository>();
            services.AddTransient<IFacultyService, FacultyRepository>();
            services.AddTransient<IGenreService, GenreRepository>();
            services.AddTransient<ILocationService, LocationRepository>();
            return services;
        }
    }
}
