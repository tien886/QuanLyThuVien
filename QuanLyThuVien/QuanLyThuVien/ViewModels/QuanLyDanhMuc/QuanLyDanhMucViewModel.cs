using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels.QuanLyDanhMuc;
using System.Collections.ObjectModel;
using System.Windows;

namespace QuanLyThuVien.ViewModels
{
    public partial class QuanLyDanhMucViewModel : ObservableObject
    {
        private readonly IGenreService _genreService;
        private readonly IBookCategoryService _categoryService;
        private readonly IFacultyService _facultyService;

        public QuanLyDanhMucViewModel(
            IGenreService genreService,
            IBookCategoryService categoryService,
            IFacultyService facultyService)
        {
            _genreService = genreService;
            _categoryService = categoryService;
            _facultyService = facultyService;

            _ = LoadDataAsync();

            RegisterMessages();
        }

        private async Task LoadDataAsync()
        {
            await LoadGenres();
            await LoadCategories();
            await LoadFaculties();
        }

        #region 1. QUẢN LÝ THỂ LOẠI (GENRES)

        [ObservableProperty] 
        private ObservableCollection<Genres> _genreList = new();

        private async Task LoadGenres()
        {
            var list = await _genreService.GetAllGenresAsync();
            GenreList = new ObservableCollection<Genres>(list);
        }

        [RelayCommand]
        private void OpenAddGenrePopup()
        {
            var vm = new GenrePopupViewModel(_genreService); // Add mode
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(vm));
        }

        [RelayCommand]
        private void OpenEditGenrePopup(Genres genre)
        {
            var vm = new GenrePopupViewModel(_genreService, genre); // Edit mode
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(vm));
        }

        [RelayCommand]
        private async Task DeleteGenre(Genres genre)
        {
            if (MessageBox.Show($"Bạn có chắc muốn xóa thể loại '{genre.GenreName}'?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    await _genreService.DeleteGenreAsync(genre.GenreID); // Giả sử service có hàm này
                    GenreList.Remove(genre);
                }
                catch { MessageBox.Show("Không thể xóa (có thể đang có sách thuộc thể loại này)."); }
            }
        }
        #endregion

        #region 2. QUẢN LÝ PHÂN LOẠI (CATEGORIES)

        [ObservableProperty] 
        private ObservableCollection<BookCategories> _categoryList = new();

        private async Task LoadCategories()
        {
            var list = await _categoryService.GetAllBookCategoriesAsync();
            CategoryList = new ObservableCollection<BookCategories>(list);
        }

        [RelayCommand]
        private void OpenAddCategoryPopup() 
        {
            var vm = new CategoryPopupViewModel(_categoryService); // Add mode
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(vm));
        }

        [RelayCommand]
        private void OpenEditCategoryPopup(BookCategories category) 
        {
            var vm = new CategoryPopupViewModel(_categoryService, category); // Edit mode
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(vm));
        }

        [RelayCommand]
        private async Task DeleteCategory(BookCategories category)
        {
            if (MessageBox.Show($"Bạn có chắc muốn xóa phân loại '{category.CategoryName}'?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    await _categoryService.DeleteCategoryAsync(category.CategoryID);
                    CategoryList.Remove(category);
                }
                catch { MessageBox.Show("Lỗi khi xóa."); }
            }
        }
        #endregion

        #region 3. QUẢN LÝ KHOA (FACULTIES)

        [ObservableProperty] private ObservableCollection<Faculties> _facultyList = new();

        private async Task LoadFaculties()
        {
            var list = await _facultyService.GetAllFacultiesAsync();
            FacultyList = new ObservableCollection<Faculties>(list);
        }

        [RelayCommand]
        private void OpenAddFacultyPopup() 
        {
            var vm = new FacultyPopupViewModel(_facultyService); // Add mode
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(vm));
        }

        [RelayCommand]
        private void OpenEditFacultyPopup(Faculties faculty) 
        {
            var vm = new FacultyPopupViewModel(_facultyService, faculty); // Edit mode
            WeakReferenceMessenger.Default.Send(new OpenDialogMessage(vm));
        }

        [RelayCommand]
        private async Task DeleteFaculty(Faculties faculty)
        {
            if (MessageBox.Show($"Bạn có chắc muốn xóa khoa '{faculty.FacultyName}'?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                     await _facultyService.DeleteFacultyAsync(faculty.FacultyID);
                    FacultyList.Remove(faculty);
                }
                catch { MessageBox.Show("Lỗi khi xóa."); }
            }
        }
        #endregion

        // --- XỬ LÝ MESSAGE ĐỂ UPDATE UI ---
        private void RegisterMessages()
        {
            // 1. Genre Messages
            WeakReferenceMessenger.Default.Register<GenreAddedMessage>(this, (r, m) => GenreList.Add(m.NewGenre));
            WeakReferenceMessenger.Default.Register<GenreUpdatedMessage>(this, (r, m) =>
            {
                _ = LoadGenres();
            });

            // 2. Category Messages
            WeakReferenceMessenger.Default.Register<CategoryAddedMessage>(this, (r, m) => CategoryList.Add(m.NewCategory));
            WeakReferenceMessenger.Default.Register<CategoryUpdatedMessage>(this, (r, m) =>
            {
                _ = LoadCategories();
            });

            // 3. Faculty Messages
            WeakReferenceMessenger.Default.Register<FacultyAddedMessage>(this, (r, m) => FacultyList.Add(m.NewFaculty));
            WeakReferenceMessenger.Default.Register<FacultyUpdatedMessage>(this, (r, m) =>
            {
                _ = LoadFaculties();
            });
        }
    }

    // --- CÁC CLASS MESSAGE  ---
    public class GenreAddedMessage { 
        public Genres NewGenre { get; } 
        public GenreAddedMessage(Genres g) => NewGenre = g; 
    }

    public class GenreUpdatedMessage { 
        public Genres UpdatedGenre { get; } 
        public GenreUpdatedMessage(Genres g) => UpdatedGenre = g; 
    }

    public class CategoryAddedMessage { 
        public BookCategories NewCategory { get; } 
        public CategoryAddedMessage(BookCategories c) => NewCategory = c; 
    }
    public class CategoryUpdatedMessage { 
        public BookCategories UpdatedCategory { get; } 
        public CategoryUpdatedMessage(BookCategories c) => UpdatedCategory = c; 
    }

    public class FacultyAddedMessage { 
        public Faculties NewFaculty { get; } 
        public FacultyAddedMessage(Faculties f) => NewFaculty = f; 
    }

    public class FacultyUpdatedMessage { 
        public Faculties UpdatedFaculty { get; } 
        public FacultyUpdatedMessage(Faculties f) => UpdatedFaculty = f; 
    }
}