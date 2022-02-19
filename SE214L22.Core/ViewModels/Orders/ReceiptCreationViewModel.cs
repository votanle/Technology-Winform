using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.ViewModels.Home;
using SE214L22.Core.ViewModels.Orders.Dtos;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SE214L22.Core.ViewModels.Orders
{
    public class ReceiptCreationViewModel : BaseViewModel
    {
        // service
        private readonly OrderService _orderService;
        private readonly ReceiptService _receiptService;

        // private fields
        private ObservableCollection<OrderForListDto> _sentOrders;
        private OrderForListDto _selectedOrder;
        private ObservableCollection<ProductForReceiptCreation> _receiptProducts;
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private int _total;

        // public property
        public ObservableCollection<OrderForListDto> SentOrders
        {
            get => _sentOrders;
            set
            {
                _sentOrders = value;
                OnPropertyChanged();
            }
        }
        public OrderForListDto SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                if (_selectedOrder != null) InitializeReceiptProduct();
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ProductForReceiptCreation> ReceiptProducts
        {
            get => _receiptProducts;
            set
            {
                _receiptProducts = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateFrom { get => _dateFrom; set { _dateFrom = value; OnPropertyChanged(); } }
        public DateTime DateTo { get => _dateTo; set { _dateTo = value; OnPropertyChanged(); } }
        public int Total { get => _total; set { _total = value; OnPropertyChanged(); } }
        public DateTime DateCreation { get => DateTime.Now.Date; }

        // command
        public ICommand SaveReceipt { get; set; }
        public ICommand RestoreData { get; set; }
        public ICommand ReloadData { get; set; }
        public ICommand SearchWithFilter { get; set; }
        public ICommand AddItem { get; set; }
        public ICommand RemoveItem { get; set; }

        public ReceiptCreationViewModel()
        {
            // serivce
            _orderService = new OrderService();
            _receiptService = new ReceiptService();

            // data
            InitialData(null);
            DateFrom = DateTime.Now.AddMonths(-3);
            DateTo = DateTime.Now;

            // command
            SaveReceipt = new RelayCommand<object>
            (
                p => SelectedOrder != null && ReceiptProducts != null && ReceiptProducts.Count > 0,
                p =>
                {
                    _receiptService.AddNewReceipt(SelectedOrder, ReceiptProducts);
                    InitialData(null);
                    HomeViewModel.getInstance().LoadData();
                }
            );

            RestoreData = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    SelectedOrder = null;
                }
            );

            ReloadData = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    InitialData(null);
                    HomeViewModel.getInstance().LoadData();
                    OrderViewModel.Instance.InitData();
                }
            );


            SearchWithFilter = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    var dateRange = new DateRangeDto
                    {
                        StartDate = DateFrom,
                        EndDate = DateTo
                    };
                    InitialData(dateRange);
                }
            );

            AddItem = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    var selectedProduct = (ProductForReceiptCreation)p;
                    selectedProduct.Number++;
                    Total = CalcTotal();
                    HomeViewModel.getInstance().LoadData();
                }
            );

            RemoveItem = new RelayCommand<object>
            (
                p => p != null && ((ProductForReceiptCreation)p).Number > 0,
                p =>
                {
                    var selectedProduct = (ProductForReceiptCreation)p;
                    selectedProduct.Number--;
                    if (selectedProduct.Number == 0)
                        ReceiptProducts.Remove(selectedProduct);
                    Total = CalcTotal();
                    HomeViewModel.getInstance().LoadData();
                }
            );

        }

        private void InitializeReceiptProduct()
        {
            ReceiptProducts = new ObservableCollection<ProductForReceiptCreation>(
                _orderService.GetOrderProducts<ProductForReceiptCreation>(SelectedOrder.Id));
            Total = CalcTotal();
        }

        private void InitialData(DateRangeDto dateRange)
        {
            var filter = new List<OrderStatus>() { OrderStatus.Sent };
            SentOrders = new ObservableCollection<OrderForListDto>(_orderService.GetOrders(filter, dateRange));

            SelectedOrder = null;

            ReceiptProducts = new ObservableCollection<ProductForReceiptCreation>();
            Total = 0;
        }

        private int CalcTotal()
        {
            var total = 0;
            foreach (var item in ReceiptProducts)
                total += item.Number * item.PriceIn;
            return total;
        }
    }
}
