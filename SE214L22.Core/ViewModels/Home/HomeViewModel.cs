using SE214L22.Core.Services;
using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.ViewModels.Home.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SE214L22.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private static HomeViewModel _instance;
        public static HomeViewModel getInstance()
        {
            return _instance;
        }
        // private service fields
        private readonly OrderService _orderService;
        private readonly InvoiceService _invoiceService;
        private readonly ProductService _productService;

        // private data fields
        private ObservableCollection<ProcessingOrderDto> _processingOrders;
        private RevenueDto _todayRevenue;
        private RevenueDto _monthRevenue;
        private ObservableCollection<HotProductDto> _hotProducts;

        // public data properties
        public ObservableCollection<ProcessingOrderDto> ProcessingOrders
        {
            get => _processingOrders;
            set
            {
                _processingOrders = value;
                OnPropertyChanged();
            }
        }
        public RevenueDto TodayRevenue
        {
            get => _todayRevenue;
            set 
            {
                _todayRevenue = value;
                OnPropertyChanged();
            }
        }

        public RevenueDto MonthRevenue
        {
            get => _monthRevenue;
            set
            {
                _monthRevenue = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<HotProductDto> HotProducts
        {
            get => _hotProducts;
            set
            {
                _hotProducts = value;
                OnPropertyChanged();
            }
        }

        // public command properties
        public ICommand ReloadData { get; set; }


        public HomeViewModel()
        {
            if (_instance == null)
                _instance = this;
            // init services
            _orderService = new OrderService();
            _invoiceService = new InvoiceService();
            _productService = new ProductService();

            LoadData();


            ReloadData = new RelayCommand<object>
            (
                p => true,
                p => LoadData()
            );
        }

        public void LoadData()
        {
            // init data
            ProcessingOrders = new ObservableCollection<ProcessingOrderDto>(_orderService.GetProcessingOrders());
            TodayRevenue = _invoiceService.GetRevenue(DateTime.Now, TimeType.Day);
            MonthRevenue = _invoiceService.GetRevenue(DateTime.Now, TimeType.Month);
            HotProducts = new ObservableCollection<HotProductDto>(_productService.GetHotProducts(DateTime.Now));
        }
    }
}
