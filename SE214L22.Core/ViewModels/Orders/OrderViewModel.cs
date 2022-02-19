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
    public class OrderViewModel : BaseViewModel
    {
        private static OrderViewModel _instance;
        public static OrderViewModel Instance 
        { 
            get 
            {
                if (_instance == null)
                    _instance = new OrderViewModel();
                return _instance;
            } 
        }

        // service
        private readonly OrderService _orderService;

        // private field
        private ObservableCollection<OrderForListDto> _orders;
        private ObservableCollection<ProductForOrderListDto> _orderProducts;
        private OrderForListDto _selectedOrder;
        private bool _waitForSent;
        private bool _sent;
        private bool _done;
        private DateTime _dateFrom;
        private DateTime _dateTo;

        // public property
        public ObservableCollection<OrderForListDto> Orders { get => _orders; set { _orders = value; OnPropertyChanged(); } }
        public ObservableCollection<ProductForOrderListDto> OrderProducts { get => _orderProducts; set { _orderProducts = value; OnPropertyChanged(); } }
        public OrderForListDto SelectedOrder 
        { 
            get => _selectedOrder; 
            set 
            { 
                _selectedOrder = value;
                if (_selectedOrder != null) LoadOrderProducts();
                OnPropertyChanged(); 
            } 
        }
        public bool WaitForSent { get => _waitForSent; set { _waitForSent = value; OnPropertyChanged(); } }
        public bool Sent { get => _sent; set { _sent = value; OnPropertyChanged(); } }
        public bool Done { get => _done; set { _done = value; OnPropertyChanged(); } }
        public DateTime DateFrom { get => _dateFrom; set { _dateFrom = value; OnPropertyChanged(); } }
        public DateTime DateTo { get => _dateTo; set { _dateTo = value; OnPropertyChanged(); } }

        // public command
        public ICommand ToggleCheckOption { get; set; }
        public ICommand SearchWithFilter { get; set; }
        public ICommand ChangeStatusToWaitForSent { get; set; }
        public ICommand ChangeStatusToSent { get; set; }
        public ICommand ChangeStatusToDone { get; set; }
        public ICommand DeleteOrder { get; set; }

        public OrderViewModel()
        {

            // service
            _orderService = new OrderService();

            // data

            InitData();

            // command
            ToggleCheckOption = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    if (p != null)
                    {
                        var checkOption = (OrderStatus)p;
                        switch (checkOption)
                        {
                            case OrderStatus.Sent:
                                Sent = true;
                                WaitForSent = Done = false;
                                break;
                            case OrderStatus.WaitForSent:
                                WaitForSent = true;
                                Sent = Done = false;
                                break;
                            case OrderStatus.Done:
                                Done = true;
                                WaitForSent = Sent = false;
                                break;
                            default:
                                break;
                        }

                        LoadOrdersWithFilter();
                        HomeViewModel.getInstance().LoadData();
                    }
                }
            );

            SearchWithFilter = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    LoadOrdersWithFilter();
                    HomeViewModel.getInstance().LoadData();
                }
            );

            ChangeStatusToWaitForSent = new RelayCommand<object>
            (
                p => SelectedOrder != null,
                p =>
                {
                    if (p != null && (bool)p)
                    {
                        SelectedOrder.Status = (int)OrderStatus.WaitForSent;
                        _orderService.UpdateOrderStatus(SelectedOrder.Id, OrderStatus.WaitForSent);
                        HomeViewModel.getInstance().LoadData();
                    }
                }
            );

            ChangeStatusToSent = new RelayCommand<object>
            (
                p => SelectedOrder != null,
                p =>
                {
                    if (p != null && (bool)p)
                    {
                        SelectedOrder.Status = (int)OrderStatus.Sent;
                        _orderService.UpdateOrderStatus(SelectedOrder.Id, OrderStatus.Sent);
                        HomeViewModel.getInstance().LoadData();
                    }
                }
            );

            ChangeStatusToDone = new RelayCommand<object>
            (
                p => SelectedOrder != null,
                p =>
                {
                    if (p != null && (bool)p)
                    {
                        SelectedOrder.Status = (int)OrderStatus.Done;
                        _orderService.UpdateOrderStatus(SelectedOrder.Id, OrderStatus.Done);
                        HomeViewModel.getInstance().LoadData();
                    }
                }
            );

            DeleteOrder = new RelayCommand<object>
            (
                p => SelectedOrder != null,
                p =>
                {
                    if (p != null && (bool)p)
                    {
                        if (SelectedOrder.Status == (int)OrderStatus.Sent || SelectedOrder.Status == (int)OrderStatus.Done)
                            throw new Exception("Không thể xóa phiếu đặt hàng đã gửi hoặc đã hoàn thành!");

                        _orderService.DeleleOrder(SelectedOrder.Id);
                        Orders.Remove(SelectedOrder);
                        SelectedOrder = null;
                        HomeViewModel.getInstance().LoadData();
                    }
                }
            );
        }

        public void InitData()
        {

            DateFrom = DateTime.Now.AddMonths(-3);
            DateTo = DateTime.Now.AddDays(1);

            WaitForSent = true;
            Sent = false;
            Done = false;

            LoadOrdersWithFilter();
            SelectedOrder = null;
            OrderProducts = new ObservableCollection<ProductForOrderListDto>();
        }

        private void LoadOrdersWithFilter()
        {
            // order status filter
            var filter = new List<OrderStatus>();
            if (WaitForSent) filter.Add(OrderStatus.WaitForSent);
            if (Sent) filter.Add(OrderStatus.Sent);
            if (Done) filter.Add(OrderStatus.Done);

            // datetime filter
            var dateRange = new DateRangeDto { StartDate = DateFrom, EndDate = DateTo };
            Orders = new ObservableCollection<OrderForListDto>(_orderService.GetOrders(filter, dateRange));

            OrderProducts = new ObservableCollection<ProductForOrderListDto>();
        }

        private void LoadOrderProducts()
        {
            OrderProducts = new ObservableCollection<ProductForOrderListDto>(
                _orderService.GetOrderProducts<ProductForOrderListDto>(SelectedOrder.Id));
        }
    }
}
