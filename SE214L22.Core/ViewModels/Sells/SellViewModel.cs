using SE214L22.Core.Services.AppCustomer;
using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.ViewModels.Customers;
using SE214L22.Core.ViewModels.Products.Dtos;
using SE214L22.Core.ViewModels.Reports;
using SE214L22.Core.ViewModels.Sells.Dtos;
using SE214L22.Data.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SE214L22.Core.ViewModels.Sells
{
    public class SellViewModel : BaseViewModel
    {
        // private service fields
        private readonly ProductService _productService;
        private readonly InvoiceService _invoiceService;
        private readonly CustomerService _customerService;
        private readonly CustomerLevelRepository _customerLevelRepository;

        // private data fields
        private List<ProductForSellDto> _loadedProducts;
        private List<bool> _loadedPages;
        private ObservableCollection<ProductForSellDto> _products;
        private int _currentPage;
        private int _totalPages;
        private int _pageSize;
        private ObservableCollection<ProductForSellDto> _storedSelectedProducts;
        private ObservableCollection<SelectingProductForSellDto> _selectedProducts;
        private InvoiceForCreationDto _invoice;
        private string _productNameKeyword;

        // public data properties

        public ObservableCollection<ProductForSellDto> Products
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

        public ObservableCollection<SelectingProductForSellDto> SelectedProducts
        {
            get => _selectedProducts;
            set
            {
                _selectedProducts = value;
                OnPropertyChanged();
            }
        }

        public InvoiceForCreationDto Invoice { get => _invoice; set { _invoice = value; OnPropertyChanged(); } }

        public string ProductNameKeyword
        {
            get => _productNameKeyword;
            set
            {
                _productNameKeyword = value;
                CurrentPage = 1;
                OnPropertyChanged();
                ReloadProducts();
            }
        }

        // public command properties
        public ICommand GoNextPage { get; set; }
        public ICommand GoPrevPage { get; set; }
        public ICommand AddItem { get; set; }
        public ICommand RemoveItem { get; set; }
        public ICommand SaveInvoice { get; set; }
        public ICommand ResetInput { get; set; }
        public ICommand GetCustomer { get; set; }

        public SellViewModel()
        {
            // service
            _productService = new ProductService();
            _invoiceService = new InvoiceService();
            _customerService = new CustomerService();

            //Repository
            _customerLevelRepository = new CustomerLevelRepository();

            // data
            _storedSelectedProducts = new ObservableCollection<ProductForSellDto>();
            ProductNameKeyword = null;

            SelectedProducts = new ObservableCollection<SelectingProductForSellDto>();

            Invoice = new InvoiceForCreationDto { Total = 0 };


            // command
            GoNextPage = new RelayCommand<object>
            (
                p => CurrentPage < TotalPages,
                p =>
                {
                    CurrentPage++;
                    if (CurrentPage > _loadedPages.Count)
                    {
                        CurrentPage = _loadedPages.Count;
                    }
                    if (_loadedPages[CurrentPage - 1] == true)
                    {
                        var start = (CurrentPage - 1) * _pageSize;
                        var end = start + _pageSize;
                        Products = new ObservableCollection<ProductForSellDto>();
                        for (int i = start; i < _loadedProducts.Count; i++)
                            if (i < end)
                                Products.Add(_loadedProducts[i]);
                    }
                    else
                    {
                        var pagedListNextPageData = GetData();

                        _loadedProducts.AddRange(pagedListNextPageData);
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
                    if (CurrentPage < 0)
                    {
                        CurrentPage = 1;
                    }
                    if (CurrentPage > _loadedPages.Count)
                    {
                        CurrentPage = _loadedPages.Count;
                    }
                    if (_loadedPages[CurrentPage - 1] == true)
                    {

                        var start = (CurrentPage - 1) * _pageSize;
                        var end = start + _pageSize;

                        Products = new ObservableCollection<ProductForSellDto>();
                        for (int i = start; i < end; i++)
                            Products.Add(_loadedProducts[i]);
                    }
                    else
                    {
                        var pagedListPrevPageData = GetData();
                        _loadedProducts.AddRange(pagedListPrevPageData);
                        _loadedPages[CurrentPage - 1] = true;
                    }

                }
            );

            AddItem = new RelayCommand<object>
            (
                p => CanAddMoreItem(p),
                p => AddMoreItem(p, 1)
            );

            RemoveItem = new RelayCommand<object>
            (
                p => true,
                p => RemoveItems(p, 1)
            );

            SaveInvoice = new RelayCommand<object>
            (
                p => SelectedProducts.Count > 0 && Invoice != null && Invoice.CustomerName?.Trim() != "" && Invoice.PhoneNumber?.Trim() != "",
                p =>
                {
                    if (p != null && (bool)p)
                    {
                        // save to DB
                        _invoiceService.AddInvoice(Invoice, SelectedProducts.ToList());

                        //Update AccumulatedPoint of customer
                        var customer = _customerService.GetCustomerByPhone(Invoice.PhoneNumber);
                        CustomerDisplayDto updatedCustomer;
                        updatedCustomer = Mapper.Map<CustomerDisplayDto>(customer);
                        updatedCustomer.AccumulatedPoint += Invoice.Price / 100000;
                        //Check and Update AccumulatedPoint
                        if (updatedCustomer.AccumulatedPoint >= _customerLevelRepository.GetCustomerLevelByName("Hạng Vàng").PointLevel)
                            updatedCustomer.CustomerLevelId = 3;
                        else if (updatedCustomer.AccumulatedPoint >= _customerLevelRepository.GetCustomerLevelByName("Hạng Bạc").PointLevel)
                            updatedCustomer.CustomerLevelId = 2;
                        else
                            updatedCustomer.CustomerLevelId = 1;
                        _customerService.UpdateCustomer(updatedCustomer);

                        // reset data
                        SelectedProducts = new ObservableCollection<SelectingProductForSellDto>();
                        Invoice = new InvoiceForCreationDto();
                        ReportViewModel.getInstance().Update();
                        CustomerViewModel.getInstance().LoadListCustomers();
                        MessageBox.Show("Thanh toán hành công");
                    }

                }
            );

            ResetInput = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    if (p != null && (bool)p)
                    {
                        // reset input
                        ResetProductNumbers();
                        SelectedProducts = new ObservableCollection<SelectingProductForSellDto>();
                        _storedSelectedProducts = new ObservableCollection<ProductForSellDto>();
                        Invoice = new InvoiceForCreationDto();
                    }

                }
            );

            GetCustomer = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    var customer = _customerService.GetCustomerByPhone(Invoice.PhoneNumber);
                    if (customer != null)
                    {
                        Invoice.CustomerName = customer.Name;
                        CalcInvoiceDiscountAndPrice();
                        Invoice.CustomerLevel = customer.CustomerLevel.Name;

                    }
                    else
                        throw new Exception("Khách hàng với số điện thoại này không tồn tại!");
                }
            );
        }

        private bool CanAddMoreItem(object p)
        {
            if (p != null)
            {
                //var selectingProduct = (ProductForSellDto)p;
                var type = p.GetType();
                if (type == typeof(ProductForSellDto))
                    return ((ProductForSellDto)p).Number > 0;
                else if (type == typeof(SelectingProductForSellDto))
                {
                    var actualProduct = _storedSelectedProducts.Where(pr => pr.Id == ((SelectingProductForSellDto)p).Id).FirstOrDefault();
                    return actualProduct != null && actualProduct.Number > 0;
                }
            }
            return false;
        }

        private void AddMoreItem(object p, int number)
        {
            var type = p.GetType();

            // get product id
            int productId = -1;
            if (type == typeof(ProductForSellDto))
                productId = ((ProductForSellDto)p).Id;
            else if (type == typeof(SelectingProductForSellDto))
                productId = ((SelectingProductForSellDto)p).Id;

            // find need-selecting products
            var product = _loadedProducts.Where(sp => sp.Id == productId).FirstOrDefault();
            var storedSelectedProduct = _storedSelectedProducts.Where(sp => sp.Id == productId).FirstOrDefault();

            // calc the max no.items can be added
            var numberCanBeAdded = -1;
            if (product != null)
            {
                numberCanBeAdded = number <= product.Number ? number : product.Number;
            }
            else
            {
                numberCanBeAdded = number <= storedSelectedProduct.Number ? number : storedSelectedProduct.Number;
            }

            // add items to cart
            var selectedProduct = SelectedProducts.Where(sp => sp.Id == productId).FirstOrDefault();
            if (selectedProduct != null)
                selectedProduct.SelectedNumber += numberCanBeAdded;
            else
            {
                _storedSelectedProducts.Add(product);
                SelectedProducts.Add(Mapper.Map<SelectingProductForSellDto>(product));
            }

            // decrease product number in products
            if (product != null)
            {
                product.Number -= numberCanBeAdded;
            }

            // For SURE
            var productInDisplayList = Products.Where(x => x.Id == product.Id).FirstOrDefault();
            if (productInDisplayList != null && productInDisplayList.Number != product.Number)
            {
                productInDisplayList.Number -= numberCanBeAdded;
            }

            if (storedSelectedProduct != null && storedSelectedProduct != product) storedSelectedProduct.Number -= numberCanBeAdded;

            CalcInvoiceTotal();
            CalcInvoiceDiscountAndPrice();
        }

        private void RemoveItems(object p, int number)
        {
            var selectedProduct = p as SelectingProductForSellDto;

            // max no.item can be removed
            var numberCanBeRemoved = number <= selectedProduct.SelectedNumber ? number : selectedProduct.SelectedNumber;

            // remove items from cart
            selectedProduct.SelectedNumber -= numberCanBeRemoved;

            // add no.items to products list
            var storedSelectedproduct = _storedSelectedProducts.Where(pr => pr.Id == selectedProduct.Id).FirstOrDefault();
            storedSelectedproduct.Number += numberCanBeRemoved;

            var loadedProduct = _loadedProducts.Where(pr => pr.Id == selectedProduct.Id).FirstOrDefault();
            if (loadedProduct != null && loadedProduct != storedSelectedproduct)
                loadedProduct.Number += numberCanBeRemoved;

            if (selectedProduct.SelectedNumber == 0)
            {
                SelectedProducts.Remove(selectedProduct);
                _storedSelectedProducts.Remove(storedSelectedproduct);

                //if (loadedProduct != null)_loadedProducts.Remove(loadedProduct);
            }


            CalcInvoiceTotal();
            CalcInvoiceDiscountAndPrice();
        }

        private void CalcInvoiceTotal()
        {
            var total = 0;
            foreach (var product in SelectedProducts)
            {
                total += product.PriceOut * product.SelectedNumber;
            }
            Invoice.Total = total;
        }
        private void CalcInvoiceDiscountAndPrice()
        {
            var customer = _customerService.GetCustomerByPhone(Invoice.PhoneNumber);
            if (customer != null)
            {
                Invoice.Discount = (int)customer.CustomerLevel.Discount * Invoice.Total / 100;
                Invoice.Price = Invoice.Total - Invoice.Discount;
            }
            else
                Invoice.Price = Invoice.Total;
        }


        private void ReloadProducts()
        {
            var pagedListData = GetData();

            _loadedProducts = new List<ProductForSellDto>(pagedListData);
            _loadedPages = new List<bool>(TotalPages);
            for (int i = 0; i < TotalPages; i++)
                _loadedPages.Add(false);
            if (TotalPages > 0) _loadedPages[0] = true;
        }

        private IEnumerable<ProductForSellDto> GetData()
        {
            var pagedList = _productService.GetProductsForSell(ProductNameKeyword, CurrentPage, 16);
            Products = new ObservableCollection<ProductForSellDto>(pagedList.Data);
            CurrentPage = pagedList.CurrentPage;
            TotalPages = pagedList.TotalPages;
            _pageSize = pagedList.PageRecords;

            // if item exists in stored selected products, just change the no. items to the one corressponding
            foreach (var item in Products)
            {
                var storedSelectedProduct = _storedSelectedProducts.Where(ssp => ssp.Id == item.Id).FirstOrDefault();
                if (storedSelectedProduct != null)
                {
                    item.Number = storedSelectedProduct.Number;
                }
            }

            // if this method is called to reload current page items, reload them in _loadItems as well
            //var start = (CurrentPage - 1) * _pageSize;

            //if (_loadedProducts != null && _loadedProducts.Count >= start)
            //{
            //    var end = _loadedProducts.Count >= start + _pageSize ? start + _pageSize : _loadedProducts.Count;
            //    for (int i = start; i < end; i++)
            //    {
            //        var product = _loadedProducts.ElementAt(i);
            //        if (product != null)
            //        {
            //            product.Number = pagedList.Data.ElementAt(i - start).Number;
            //        }
            //    }
            //}

            return pagedList.Data;
        }

        private void ResetProductNumbers()
        {
            foreach (var item in _selectedProducts)
            {
                var storedProduct = _loadedProducts.Where(x => x.Id == item.Id).FirstOrDefault();
                //var displayedProduct = Products.Where(x => x.Id == item.Id).FirstOrDefault();

                storedProduct.Number += item.SelectedNumber;
                //displayedProduct.Number += item.Number;
            }
        }

    }
}
