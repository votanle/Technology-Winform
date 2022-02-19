using Microsoft.Win32;
using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.ViewModels.Products.Dtos;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Shared.AppConsts;
using SE214L22.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SE214L22.Core.ViewModels.Products
{
    public class ProductViewModel : BaseViewModel
    {
        // private service fields
        private readonly ProductService _productService;
        private readonly ManufacturerService _manufacturerService;
        private readonly CategoryService _categoryService;

        // private data fields
        private List<ProductDisplayDto> _loadedProducts;
        private List<bool> _loadedPages;
        private ObservableCollection<ProductDisplayDto> _products;
        private ObservableCollection<Manufacturer> _manufacturers;
        private ProductDisplayDto _selectedProduct;
        private ObservableCollection<Category> _categories;
        private ProductForCreationDto _newProduct;
        private Manufacturer _selectedManufacturer;
        private Category _selectedCategory;
        private int _currentPage;
        private int _totalPages;
        private int _pageSize;
        private string _productNameKeyword;
        private List<Category> _filterCategory;
        private List<Manufacturer> _filterManufacturer;

        // public data properties

        public ObservableCollection<ProductDisplayDto> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }
        public ProductForCreationDto NewProduct
        {
            get => _newProduct;
            set
            {
                _newProduct = value;
                OnPropertyChanged();
            }
        }

        public ProductDisplayDto SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                if (value != null)
                    _selectedProduct.Photo = GetPhotoPath(value.Photo);
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Manufacturer> Manufacturers
        {
            get => _manufacturers;
            set
            {
                _manufacturers = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }
        public Manufacturer SelectedManufacturer
        {
            get => _selectedManufacturer;
            set
            {
                _selectedManufacturer = value;
                OnPropertyChanged();
            }
        }
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }
        public List<Category> FilterCategory
        {
            get => _filterCategory;
            set 
            {
                _filterCategory = value;
                OnPropertyChanged();
                LoadListProducts();
            }
        }
        public List<Manufacturer> FilterManufacturer
        {
            get => _filterManufacturer;
            set
            {
                _filterManufacturer = value;
                OnPropertyChanged();
                LoadListProducts();
            }
        }
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        public string ProductNameKeyword
        {
            get => _productNameKeyword;
            set
            {
                _productNameKeyword = value;
                OnPropertyChanged();                
                LoadListProducts();
            }
        }

        // public command properties
        public ICommand GoNextPage { get; set; }
        public ICommand GoPrevPage { get; set; }
        public ICommand SelectPhotoCreate { get; set; }
        public ICommand SelectPhotoUpdate { get; set; }
        public ICommand AddProduct { get; set; }
        public ICommand UpdateProduct { get; set; }
        public ICommand ReloadProducts { get; set; }
        public ICommand PrepareAddProduct { get; set; }
        public ICommand PrepareUpdateProduct { get; set; }
        public ICommand HideProduct { get; set; }
        public ICommand UpdateData { get; set; }
        public ICommand ResetReturnRateAdd { get; set; }
        public ICommand ResetReturnRateEdit { get; set; }

        public ProductViewModel()
        {
            // service
            _productService = new ProductService();
            _manufacturerService = new ManufacturerService();
            _categoryService = new CategoryService();
            NewProduct = new ProductForCreationDto();


            // data            
            Manufacturers = new ObservableCollection<Manufacturer>(_manufacturerService.GetManufacturers());
            Categories = new ObservableCollection<Category>(_categoryService.GetCategories());
            FilterCategory = new List<Category>();
            FilterManufacturer = new List<Manufacturer>();
            

            LoadListProducts();

            // command            
            GoNextPage = new RelayCommand<object>
            (
                p => CurrentPage < TotalPages,
                p =>
                {
                    CurrentPage++;

                    if (_loadedPages[CurrentPage - 1] == true)
                    {
                        var start = (CurrentPage - 1) * _pageSize;
                        var end = start + _pageSize;
                        Products = new ObservableCollection<ProductDisplayDto>();
                        for (int i = start; i < _loadedProducts.Count; i++)
                            if (i < end)
                                Products.Add(_loadedProducts[i]);
                    }
                    else
                    {
                        var pagedListNextPage = _productService.GetProductsForDisplayProduct(CurrentPage);
                        Products = new ObservableCollection<ProductDisplayDto>(pagedListNextPage.Data);

                        _loadedProducts.AddRange(pagedListNextPage.Data);
                        _loadedPages[CurrentPage - 1] = true;
                    }
                }


            );

            GoPrevPage = new RelayCommand<object>
            (
                p => CurrentPage > 1,
                p =>
                {
                    CurrentPage--;
                    if (_loadedPages[CurrentPage - 1] == true)
                    {

                        var start = (CurrentPage - 1) * _pageSize;
                        var end = start + _pageSize;

                        Products = new ObservableCollection<ProductDisplayDto>();
                        for (int i = start; i < end; i++)
                            Products.Add(_loadedProducts[i]);
                    }
                    else
                    {
                        var pagedListPrevPage = _productService.GetProductsForDisplayProduct(CurrentPage);
                        Products = new ObservableCollection<ProductDisplayDto>(pagedListPrevPage.Data);
                        _loadedProducts.AddRange(pagedListPrevPage.Data);
                        _loadedPages[CurrentPage - 1] = true;
                    }

                }
            );
            SelectPhotoCreate = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                    if (fileDialog.ShowDialog() == true)
                    {
                        NewProduct.Photo = fileDialog.FileName;

                    }
                    else
                    {
                        NewProduct.Photo = null;
                    }
                }
            );
            SelectPhotoUpdate = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                    if (fileDialog.ShowDialog() == true)
                    {
                        SelectedProduct.Photo = fileDialog.FileName;

                    }
                    else
                    {
                        SelectedProduct.Photo = null;
                    }
                }
            );
            PrepareAddProduct = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    Manufacturers = new ObservableCollection<Manufacturer>(_manufacturerService.GetManufacturers());
                    Categories = new ObservableCollection<Category>(_categoryService.GetCategories());
                    NewProduct = new ProductForCreationDto();
                    NewProduct.Photo = GetPhotoPath(DefaultPhotoNames.Product);
                }
            );
            AddProduct = new RelayCommand<object>
            (
                p => 
                {
                    if (SelectedCategory == null || SelectedManufacturer == null)
                        return false;
                    return true;
                },
                p =>
                {
                    if (p != null && (bool)p == true)  
                    {
                        NewProduct.CategoryId = SelectedCategory.Id;
                        NewProduct.ManufacturerId = SelectedManufacturer.Id;
                        _productService.AddProduct(NewProduct);
                        MessageBox.Show("Thêm sản phẩm thành công");
                    }

                }
            );

            PrepareUpdateProduct = new RelayCommand<object>
            (
                p =>
                {
                    if (SelectedProduct == null) return false;
                    return true;
                },
                p =>
                {
                    Manufacturers = new ObservableCollection<Manufacturer>(_manufacturerService.GetManufacturers());
                    Categories = new ObservableCollection<Category>(_categoryService.GetCategories());
                    SelectedManufacturer = Manufacturers.Where(m => m.Id == SelectedProduct.ManufacturerId).FirstOrDefault();
                    SelectedCategory = Categories.Where(c => c.Id == SelectedProduct.CategoryId).FirstOrDefault();
                    Console.WriteLine("photo");
                    Console.WriteLine(SelectedProduct.Photo);

                }
            );
            UpdateProduct = new RelayCommand<object>
            (
                p =>
                {
                    if (SelectedProduct == null) return false;
                    return true;
                },
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        _productService.UpdateProduct(SelectedProduct);
                        MessageBox.Show("Sửa sản phẩm thành công");
                    }
                }
            );

            HideProduct = new RelayCommand<object>
            (
                p =>
                {
                    if (SelectedProduct == null) return false;
                    return true;
                },
                p =>
                {
                    if (p != null && p.GetType() == typeof(bool) && (bool)p == true)
                    {
                        _productService.HidenProduct(SelectedProduct);
                        MessageBox.Show("Xóa sản phẩm thành công");
                    }
                }
            );

            ReloadProducts = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    LoadListProducts();
                }
            );

            UpdateData = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    Manufacturers = new ObservableCollection<Manufacturer>(_manufacturerService.GetManufacturers());
                    Categories = new ObservableCollection<Category>(_categoryService.GetCategories());
                }
            );
            ResetReturnRateAdd = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    NewProduct.ReturnRate = null;
                }
            );
            ResetReturnRateEdit = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    SelectedProduct.ReturnRate = SelectedProduct.Category.ReturnRate;
                    SelectedProduct.CheckReturnRateChange = "";
                }
            );
        }
        private void LoadListProducts()
        {
            ProductFilterDto filter = new ProductFilterDto();             
            if (ProductNameKeyword != null && ProductNameKeyword != "")
            {
                filter.NameProductKeyWord = ProductNameKeyword;
            }
            if (FilterCategory != null && FilterCategory.Count() > 0) 
            {
                filter.ListCategory = new List<string>();
                foreach (var category in FilterCategory)
                {
                    filter.ListCategory.Add(category.Name);
                }
            }
            if (FilterManufacturer != null && FilterManufacturer.Count() > 0)
            {
                filter.ListManufacturer = new List<string>();
                foreach (var manufacturer in FilterManufacturer)
                {
                    filter.ListManufacturer.Add(manufacturer.Name);
                }
            }
            
            var pagedList = _productService.GetProductsForDisplayProduct(1, 13, filter);
            Products = new ObservableCollection<ProductDisplayDto>(pagedList.Data);
            CurrentPage = pagedList.CurrentPage;
            TotalPages = pagedList.TotalPages;
            _pageSize = pagedList.PageRecords;

            _loadedProducts = new List<ProductDisplayDto>(pagedList.Data);
            _loadedPages = new List<bool>(TotalPages);
            for (int i = 0; i < TotalPages; i++)
                _loadedPages.Add(false);
            if(TotalPages != 0)
                _loadedPages[0] = true;
            
        }



        private string GetPhotoPath(string fileName)
        {
            string destPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string destinationFile = Path.Combine(destPath, "Photos", "Products", fileName);
            return destinationFile;
        }
    }

}