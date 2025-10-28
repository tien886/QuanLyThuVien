

using System.Diagnostics;

namespace QuanLyThuVien.ViewModels
{
    public class QuanLySinhVienViewModel : ViewModelBase
    {
        private string _searhBarText = "";
        public string SearchBarText
        {
            get => _searhBarText;
            set
            {
                if (_searhBarText != value)
                {
                    _searhBarText = value;
                    OnPropertyChanged();
                    Debug.WriteLine(SearchBarText);
                }
            }
        }
    }
}
