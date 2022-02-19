using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.ViewModels.Settings.Dtos;
using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SE214L22.Core.ViewModels.Settings
{
    public class CategoryViewModel : BaseViewModel
    {
        // private service fields
        private CategoryService _categoryService;

        // private data fields
        private ObservableCollection<CategoryForDisplayDto> _categories;
        private CategoryForDisplayDto _chosenCategory;
        private CategoryForCreationDto _newCategory;


        // public data properties
        public ObservableCollection<CategoryForDisplayDto> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }
        public CategoryForDisplayDto ChosenCategory
        {
            get => _chosenCategory;
            set
            {
                _chosenCategory = value;
                OnPropertyChanged();
            }
        }
        public CategoryForCreationDto NewCategory
        {
            get => _newCategory;
            set
            {
                _newCategory = value;
                OnPropertyChanged();
            }
        }


        // public command properties
        public ICommand DeleteCategory { get; set; }
        public ICommand AddCategory { get; set; }
        public ICommand PrepareAddCategory { get; set; }
        public ICommand UpdateCategory { get; set; }
        public ICommand PrepareUpdateCategory { get; set; }

        public CategoryViewModel()
        {
            _categoryService = new CategoryService();

            Categories = new ObservableCollection<CategoryForDisplayDto>(_categoryService.GetDisplayCategories());
            NewCategory = new CategoryForCreationDto { };

            DeleteCategory = new RelayCommand<object>
            (
                p => ChosenCategory == null ? false : true,
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        _categoryService.DeleteCategory(ChosenCategory);
                        Categories = new ObservableCollection<CategoryForDisplayDto>(_categoryService.GetDisplayCategories());
                        MessageBox.Show("Xóa loại mặt hàng thành công");
                    }
                }
            );

            PrepareAddCategory = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    NewCategory = new CategoryForCreationDto { };
                }
             );

            AddCategory = new RelayCommand<object>
            (
                p =>
                {
                    if (NewCategory.Name == null || NewCategory.Description == null || NewCategory.ReturnRate == null)
                        return false;
                    return true;
                },
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        _categoryService.AddCategory(NewCategory);
                        Categories = new ObservableCollection<CategoryForDisplayDto>(_categoryService.GetDisplayCategories());
                        MessageBox.Show("Thêm loại mặt hàng thành công");
                    }
                }
            );
            PrepareUpdateCategory = new RelayCommand<object>
            (
                p => ChosenCategory == null ? false : true,
                p =>
                {
                }
             );
            UpdateCategory = new RelayCommand<object>
            (
                p => ChosenCategory == null ? false : true,
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        _categoryService.UpdateCategory(ChosenCategory);
                        Categories = new ObservableCollection<CategoryForDisplayDto>(_categoryService.GetDisplayCategories());
                        MessageBox.Show("Cập nhật loại mặt hàng thành công");
                    }
                }
             );
        }
    }
}
