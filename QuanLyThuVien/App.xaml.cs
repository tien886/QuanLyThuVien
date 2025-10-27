using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.Config;
using QuanLyThuVien.DI;
using QuanLyThuVien.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyThuVien
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            services.ConfigureServices();
            ServiceProvider = services.BuildServiceProvider();
            var dbservice = ServiceProvider.GetRequiredService<DatabaseConfig>();
            await dbservice.Initialize();
            var main = ServiceProvider.GetRequiredService<MainView>();
            main.Show();
        }
    }
}
