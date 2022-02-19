using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.ViewModels.Home;
using SE214L22.Core.ViewModels.Orders.Dtos;
using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SE214L22.Core.ViewModels.Orders
{
    public class OrderCreationViewModel : BaseViewModel
    {
        // private service fields
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        private readonly ProviderService _providerService;

        // private data fields
        private List<ProductForOrderCreationDto> _loadedProducts;
        private List<bool> _loaded;
        private int _pageSize;

        private ObservableCollection<ProductForOrderCreationDto> _products;
        private int _currentPage;
        private int _totalPages;
        private ObservableCollection<SelectingProductDto> _selectedProducts;
        private OrderForCreationDto _order;
        
        private ObservableCollection<Provider> _providers;
        private Provider _selectingProvider;

        private string _productNameKeyword;

        // public data properties
        public ObservableCollection<ProductForOrderCreationDto> Products
        { 
            get => _products; 
            set
            {
                _products = value;
                OnPropertyChanged();
            } 
        }

        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        public ObservableCollection<SelectingProductDto> SelectedProducts
        {
            get => _selectedProducts;
            set
            {
                _selectedProducts = value;
                OnPropertyChanged();
            }
        }

        public OrderForCreationDto Order { get => _order; set { _order = value; OnPropertyChanged(); }}

        public ObservableCollection<Provider> Providers
        {
            get => _providers;
            set
            {
                _providers = value;
                OnPropertyChanged();
            }
        }
        public Provider SelectingProvider { get => _selectingProvider; set { _selectingProvider = value; OnPropertyChanged(); } }

        public string ProductNameKeyword
        {
            get => _productNameKeyword;
            set
            {
                _productNameKeyword = value;
                CurrentPage = 1;
                OnPropertyChanged();
                ReloadProduct();
            }
        }

        // public command properties

        public ICommand GoNextPage { get; set; }
        public ICommand GoPrevPage { get; set; }
        public ICommand AddItem { get; set; }
        public ICommand AddMulItems { get; set; }
        public ICommand RemoveItem { get; set; }
        public ICommand RemoveMulItems { get; set; }
        public ICommand SaveOrderInfo { get; set; }
        public ICommand UpdateOrderInfo { get; set; }
        public ICommand LoadDataForCreation { get; set; }
        public ICommand LoadDataForUpdation { get; set; }
        public ICommand ResetData { get; set; }
        public ICommand RestoreUpdationData { get; set; }
       


        public OrderCreationViewModel()
        {
            // service
            _productService = new ProductService();
            _orderService = new OrderService();
            _providerService = new ProviderService();


            // data
            ProductNameKeyword = null;

            SelectedProducts = new ObservableCollection<SelectingProductDto>();

            Providers = new ObservableCollection<Provider>(_providerService.GetProviders());
            SelectingProvider = Providers.Count > 0 ? Providers[0] : null;
            Order = new OrderForCreationDto { CreationTime = DateTime.Now.Date };



            // Command
            GoNextPage = new RelayCommand<object>(
            p => CurrentPage < TotalPages,
            p => {

                CurrentPage = CurrentPage + 1;
                if (_loaded[CurrentPage - 1] == true)
                {
                    Products = new ObservableCollection<ProductForOrderCreationDto>();
                    var start = (CurrentPage - 1) * _pageSize;
                    var end = start + _pageSize;

                    for (var i = start; i < _loadedProducts.Count; i++)
                        if (i < end)
                            Products.Add(_loadedProducts[i]);
                }
                else
                {
                    var pagedListNextPageData = GetData();

                    _loadedProducts.AddRange(pagedListNextPageData);
                    _loaded[CurrentPage - 1] = true;
                }
                
                
            }) ;

            GoPrevPage = new RelayCommand<object>(
            p => CurrentPage > 1,
            p => {




                CurrentPage--;
                if (_loaded[CurrentPage - 1] == true)
                {

                    Products = new ObservableCollection<ProductForOrderCreationDto>();
                    var start = (CurrentPage - 1) * _pageSize;
                    var end = start + _pageSize;

                    for (var i = start; i < end; i++)
                        Products.Add(_loadedProducts[i]);
                }
                else
                {
                    var pagedListPrevPageData = GetData();
                    _loadedProducts.AddRange(pagedListPrevPageData);
                    _loaded[CurrentPage - 1] = true;
                }
            });

            AddItem = new RelayCommand<object>(
                p => true,
                p => AddItems(p, 1)
            );
            AddMulItems = new RelayCommand<object>(
                p => true,
                p => AddItems(p, 10)
            );
            RemoveItem = new RelayCommand<object>(
                p => true,
                p => RemoveItems(p, 1)
            );
            RemoveMulItems = new RelayCommand<object>(
                p => true,
                p => RemoveItems(p, 10)
            );


            SaveOrderInfo = new RelayCommand<object>(
            p => 
            {
                if (SelectingProvider == null || SelectedProducts.Count == 0)
                    return false;
                return true;
            },
            p =>
            {
                if (p != null && (bool)p)
                {
                    Order.ProviderId = SelectingProvider.Id;
                    Order.UserId = CurrentUser.Id;
                    _orderService.AddNewOrder(Order, SelectedProducts);
                }
                HomeViewModel.getInstance().LoadData();
                OrderViewModel.Instance.InitData();
            });

            LoadDataForUpdation = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    if (p != null)
                    {
                        ProductNameKeyword = null;
                        LoadDataForUpdate((int)p);
                    }

                }
            );

            LoadDataForCreation = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    if (p != null && (bool)p)
                    {
                        ProductNameKeyword = null;
                        SelectedProducts = new ObservableCollection<SelectingProductDto>();
                        Order = new OrderForCreationDto { CreationTime = DateTime.Now.Date };
                    }
                }
            );

            UpdateOrderInfo = new RelayCommand<object>
            (
                p =>
                {
                    if (SelectingProvider == null || SelectedProducts.Count == 0)
                        return false;
                    return true;
                },
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        Order.ProviderId = SelectingProvider.Id;
                        _orderService.UpdateOrderInfo(Order, SelectedProducts);
                    }
                    HomeViewModel.getInstance().LoadData();
                }
            );

            ResetData = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    if (p != null && (bool)p == true)
                        SelectedProducts = new ObservableCollection<SelectingProductDto>();
                    HomeViewModel.getInstance().LoadData();
                }

            );

            RestoreUpdationData = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    if (p != null && (bool)p == true)
                        LoadDataForUpdate(Order.Id);
                }
            );
        }

        private void AddItems(object p, int number)
        {
            var type = p.GetType();

            if (p != null)
            {
                // get product id
                int productId = -1;
                if (type == typeof(ProductForOrderCreationDto))
                    productId = ((ProductForOrderCreationDto)p).Id;
                else if (type == typeof(SelectingProductDto))
                    productId = ((SelectingProductDto)p).Id;

                // change no. product in 2 lists
                var product = _loadedProducts.Where(sp => sp.Id == productId).FirstOrDefault();

                var selectedProduct = SelectedProducts.Where(sp => sp.Id == productId).FirstOrDefault();
                if (selectedProduct != null)
                    selectedProduct.SelectedNumber += number;
                else
                    SelectedProducts.Add(_orderService.SelectProduct(product));
                HomeViewModel.getInstance().LoadData();

            }
        }
        private void RemoveItems(object p, int number)
        {
            var selectedProduct = (SelectingProductDto)p;
            selectedProduct.SelectedNumber -= number;
            if (selectedProduct.SelectedNumber <= 0)
                SelectedProducts.Remove(selectedProduct);
        }

        private void LoadDataForUpdate(int orderId)
        {
            Order = _orderService.GetOrderById<OrderForCreationDto>(orderId);
            if (Order.Status == (int)OrderStatus.Sent || Order.Status == (int)OrderStatus.Done)
                throw new Exception("Không thể thay đổi phiếu đặt hàng đã gửi hoặc đã hoàn thành!");

            Order.Id = orderId;
            SelectedProducts = new ObservableCollection<SelectingProductDto>(_orderService.GetOrderProducts<SelectingProductDto>(orderId));
            SelectingProvider = Providers.Where(p => p.Id == Order.ProviderId).FirstOrDefault();
        }

        private void ReloadProduct()
        {
            var pagedListData = GetData();

            _loadedProducts = new List<ProductForOrderCreationDto>();
            _loadedProducts.AddRange(pagedListData);
            _loaded = new List<bool>();
            for (int i = 0; i < TotalPages; i++) _loaded.Add(false);
            if (TotalPages > 0) _loaded[0] = true;
            HomeViewModel.getInstance().LoadData();
        }

        private IEnumerable<ProductForOrderCreationDto> GetData()
        {
            var pagedList = _productService.GetProductsForOrderCreation(ProductNameKeyword, CurrentPage, null);
            CurrentPage = pagedList.CurrentPage;
            TotalPages = pagedList.TotalPages;
            _pageSize = pagedList.PageRecords;
            Products = new ObservableCollection<ProductForOrderCreationDto>(pagedList.Data);

            return pagedList.Data;
        }
    }
}
