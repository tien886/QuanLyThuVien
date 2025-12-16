using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QuanLyThuVien.ViewModels
{
    public partial class ClockDateState : ObservableObject
    {
        private readonly DispatcherTimer _timer;

        [ObservableProperty] private string currentDate;
        [ObservableProperty] private string currentTime;

        public ClockDateState()
        {
            UpdateNow();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += OnTick;
            _timer.Start();
        }

        private void OnTick(object? sender, EventArgs e) => UpdateNow();

        private void UpdateNow()
        {
            var now = DateTime.Now;
            CurrentDate = now.ToString("dd/MM/yyyy");
            CurrentTime = now.ToString("HH:mm:ss");
        }

        public void Dispose()
        {
            _timer.Tick -= OnTick;
            _timer.Stop();
        }
    }
}
