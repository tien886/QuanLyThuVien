using Microsoft.Extensions.DependencyInjection;
using QuanLyThuVien.Config;
using QuanLyThuVien.DI;
using QuanLyThuVien.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace QuanLyThuVien_migration
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
            SQLitePCL.Batteries.Init();
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
