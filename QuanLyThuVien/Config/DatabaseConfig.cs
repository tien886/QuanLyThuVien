using QuanLyThuVien.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Linq;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;

namespace QuanLyThuVien.Config
{
    public class DatabaseConfig
    {
        private SqliteConnection _sqliteConnection;
        private Data.DataContext _dataContext;
        public Data.DataContext DataContext => _dataContext ?? throw new ArgumentNullException("Database not initialized!");
        public static string GetDefaultDatabasePath()
        {
            string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                              AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(appDirectory, @"..\..\Resources\Database");
            string databaseDirectory = Path.GetFullPath(relativePath);
            Directory.CreateDirectory(databaseDirectory);
            Debug.WriteLine($"App Directory: {databaseDirectory}");
            return Path.Combine(databaseDirectory, "QuanLyThuVien.db");
        }
        public async Task Initialize(string dbPath = null)
        {
            try
            {
                dbPath = GetDefaultDatabasePath();
                Debug.WriteLine($"DB PATH: {dbPath}");
                _sqliteConnection = new SqliteConnection($"Data Source={dbPath}");
                if (_sqliteConnection != null && _sqliteConnection.State == System.Data.ConnectionState.Open)
                    return;
                await _sqliteConnection.OpenAsync();

                var dbOptions = new DbContextOptionsBuilder<Data.DataContext>()
                    .UseSqlite(_sqliteConnection)
                    .EnableSensitiveDataLogging()
                    .Options;

                _dataContext = new Data.DataContext(dbOptions);
                await _dataContext.Database.EnsureCreatedAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception($"Database initialization failed: {ex.Message}", ex);
            }
        }
        public void CloseConnection()
        {
            if (_sqliteConnection != null)
            {
                _sqliteConnection.Close();
                _sqliteConnection = null;
            }
        }
    }
}
