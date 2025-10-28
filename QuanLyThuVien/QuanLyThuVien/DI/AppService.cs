﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.Config;
using QuanLyThuVien.Data;
using QuanLyThuVien.Repositories;
using QuanLyThuVien.Repositories;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels;
using QuanLyThuVien.Views;
using System;

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
            //Register ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<DashBoardViewModel>();
            services.AddTransient<MuonTraSachViewModel>();
            services.AddTransient<QuanLySachViewModel>();
            services.AddTransient<QuanLySinhVienViewModel>();
            // Register Services
            services.AddTransient<IBookService, BookRepository>();
            services.AddTransient<IBookCopyService, BookCopyRepository>();
            services.AddTransient<IBookCategoryService, BookCategoryRepository>();
            return services;
        }
    }
}
