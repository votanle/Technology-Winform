using SE214L22.Core.Services.AppCustomer;
using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.ViewModels.Customers.Dtos;
using SE214L22.Core.ViewModels.Products.Dtos;
using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SE214L22.Core.ViewModels.Customers
{
    public class CustomerViewModel : BaseViewModel
    {

        private static CustomerViewModel _instance;
        public static CustomerViewModel getInstance()
        {
            return _instance;
        }
        // private service fields
        private readonly CustomerService _customerService;
        private readonly CustomerLevelService _customerLevelService;

        // private data fields
        private List<CustomerDisplayDto> _loadedCustomers;
        private List<bool> _loadedPages;
        private ObservableCollection<CustomerDisplayDto> _customers;
        private CustomerDisplayDto _selectedCustomer;
        private ObservableCollection<CustomerLevel> _customerLevel;
        private CustomerForCreationDto _newCustomer;
        private CustomerLevel _selectedCustomerLevel;
        private int _currentPage;
        private int _totalPages;
        private int _pageSize;
        private string _customerNameKeyword;
        private List<CustomerLevel> _filterCustomerLevel;

        // public data properties

        public ObservableCollection<CustomerDisplayDto> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }
        public CustomerForCreationDto NewCustomer
        {
            get => _newCustomer;
            set
            {
                _newCustomer = value;
                OnPropertyChanged();
            }
        }

        public CustomerDisplayDto SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<CustomerLevel> CustomerLevel
        {
            get => _customerLevel;
            set
            {
                _customerLevel = value;
                OnPropertyChanged();
            }
        }
        public CustomerLevel SelectedCustomerLevel
        {
            get => _selectedCustomerLevel;
            set
            {
                _selectedCustomerLevel = value;
                OnPropertyChanged();
            }
        }
        public List<CustomerLevel> FilterCustomerLevel
        {
            get => _filterCustomerLevel;
            set
            {
                _filterCustomerLevel = value;
                OnPropertyChanged();
                LoadListCustomers();
            }
        }
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        public string CustomerNameKeyword
        {
            get => _customerNameKeyword;
            set
            {
                _customerNameKeyword = value;
                OnPropertyChanged();
                LoadListCustomers();
            }
        }

        // public command properties
        public ICommand GoNextPage { get; set; }
        public ICommand GoPrevPage { get; set; }
        public ICommand AddCustomer { get; set; }
        public ICommand UpdateCustomer { get; set; }
        public ICommand ReloadCustomers { get; set; }
        public ICommand PrepareAddCustomer { get; set; }
        public ICommand PrepareUpdateCustomer { get; set; }
        public ICommand HideCustomer { get; set; }
        public ICommand UpdateData { get; set; }
        public ICommand ResetReturnRateAdd { get; set; }
        public ICommand ResetReturnRateEdit { get; set; }

        public CustomerViewModel()
        {
            if (_instance == null)
                _instance = this;
            // service
            _customerService = new CustomerService();
            _customerLevelService = new CustomerLevelService();
            NewCustomer = new CustomerForCreationDto();


            // data            
            CustomerLevel = new ObservableCollection<CustomerLevel>(_customerLevelService.GetCustomerLevels());
            FilterCustomerLevel = new List<CustomerLevel>();


            LoadListCustomers();

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
                        Customers = new ObservableCollection<CustomerDisplayDto>();
                        for (int i = start; i < _loadedCustomers.Count; i++)
                            if (i < end)
                                Customers.Add(_loadedCustomers[i]);
                    }
                    else
                    {
                        var pagedListNextPage = _customerService.GetCustomersForDisplayCustomer(CurrentPage);
                        Customers = new ObservableCollection<CustomerDisplayDto>(pagedListNextPage.Data);

                        _loadedCustomers.AddRange(pagedListNextPage.Data);
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

                        Customers = new ObservableCollection<CustomerDisplayDto>();
                        for (int i = start; i < end; i++)
                            Customers.Add(_loadedCustomers[i]);
                    }
                    else
                    {
                        var pagedListPrevPage = _customerService.GetCustomersForDisplayCustomer(CurrentPage);
                        Customers = new ObservableCollection<CustomerDisplayDto>(pagedListPrevPage.Data);
                        _loadedCustomers.AddRange(pagedListPrevPage.Data);
                        _loadedPages[CurrentPage - 1] = true;
                    }

                }
            );
            PrepareAddCustomer = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    CustomerLevel = new ObservableCollection<CustomerLevel>(_customerLevelService.GetCustomerLevels());
                    NewCustomer = new CustomerForCreationDto();
                }
            );
            AddCustomer = new RelayCommand<object>
            (
                p =>
                {
                    return true;
                },
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        NewCustomer.CustomerLevelId = 1;
                        NewCustomer.CreationTime = DateTime.Now;
                        NewCustomer.AccumulatedPoint = 0;
                        _customerService.AddCustomer(NewCustomer);
                    }

                }
            );

            UpdateCustomer = new RelayCommand<object>
            (
                p =>
                {
                    if (SelectedCustomer == null) return false;
                    return true;
                },
                p =>
                {
                    CustomerLevel = new ObservableCollection<CustomerLevel>(_customerLevelService.GetCustomerLevels());
                    SelectedCustomerLevel = CustomerLevel.Where(c => c.Id == SelectedCustomer.CustomerLevelId).FirstOrDefault();
                    if (p != null && (bool)p == true)
                    {
                        _customerService.UpdateCustomer(SelectedCustomer);
                        MessageBox.Show("Sửa thông tin khách hàng thành công");
                        LoadListCustomers();
                    }
                }
            );

            HideCustomer = new RelayCommand<object>
            (
                p =>
                {
                    if (SelectedCustomer == null) return false;
                    return true;
                },
                p =>
                {
                    if (p != null && p.GetType() == typeof(bool) && (bool)p == true)
                    {
                        _customerService.HidenCustomer(SelectedCustomer);
                        MessageBox.Show("Xóa khách hàng thành công");
                        LoadListCustomers();
                    }
                }
            );

            ReloadCustomers = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    LoadListCustomers();
                }
            );

            UpdateData = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    CustomerLevel = new ObservableCollection<CustomerLevel>(_customerLevelService.GetCustomerLevels());
                }
            );
        }
        public void LoadListCustomers()
        {
            CustomerFilterDto filter = new CustomerFilterDto();
            if (CustomerNameKeyword != null && CustomerNameKeyword != "")
            {
                filter.NameCustomerKeyWord = CustomerNameKeyword;
            }            

            var pagedList = _customerService.GetCustomersForDisplayCustomer(1, 15, filter);
            Customers = new ObservableCollection<CustomerDisplayDto>(pagedList.Data);
            CurrentPage = pagedList.CurrentPage;
            TotalPages = pagedList.TotalPages;
            _pageSize = pagedList.PageRecords;

            _loadedCustomers = new List<CustomerDisplayDto>(pagedList.Data);
            _loadedPages = new List<bool>(TotalPages);
            for (int i = 0; i < TotalPages; i++)
                _loadedPages.Add(false);
            if (TotalPages != 0)
                _loadedPages[0] = true;

        }
    }

}