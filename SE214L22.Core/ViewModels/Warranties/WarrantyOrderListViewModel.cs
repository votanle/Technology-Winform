using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.ViewModels.Warranties.Dtos;
using SE214L22.Data.Entity.AppCustomer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SE214L22.Core.ViewModels.Warranties
{
    public class WarrantyOrderListViewModel : BaseViewModel
    {
        // service
        private readonly WarrantyService _warrantyService;

        // private field
        private ObservableCollection<ProductForListWarrantyDto> _warrantyOrders;
        private ProductForListWarrantyDto _selectedWarrantyOrder;
        private int _numberOfWaitForSent;
        private int _numberOfSent;
        private int _numberOfWaitForCustomer;
        private bool _waitForSent;
        private bool _sent;
        private bool _waitForCustomer;

        // public property
        public ObservableCollection<ProductForListWarrantyDto> WarrantyOrders
        {
            get => _warrantyOrders;
            set
            {
                _warrantyOrders = value;
                OnPropertyChanged();
            }
        }

        public ProductForListWarrantyDto SelectedWarrantyOrder
        {
            get => _selectedWarrantyOrder;
            set
            {
                _selectedWarrantyOrder = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfWaitForSent { get => _numberOfWaitForSent; set { _numberOfWaitForSent = value; OnPropertyChanged(); } }
        public int NumberOfSent { get => _numberOfSent; set { _numberOfSent = value; OnPropertyChanged(); } }
        public int NumberOfWaitForCustomer { get => _numberOfWaitForCustomer; set { _numberOfWaitForCustomer = value; OnPropertyChanged(); } }
        public bool WaitForSent { get => _waitForSent; set { _waitForSent = value; OnPropertyChanged(); LoadData(); } }
        public bool Sent { get => _sent; set { _sent = value; OnPropertyChanged(); LoadData(); } }
        public bool WaitForCustomer { get => _waitForCustomer; set { _waitForCustomer = value; OnPropertyChanged(); LoadData(); } }


        // command
        public ICommand ReloadData { get; set; }
        public ICommand ChangeStatusToWaitForSent { get; set; }
        public ICommand ChangeStatusToSent { get; set; }
        public ICommand ChangeStatusToWaitForCustomer { get; set; }
        public ICommand ChangeStatusToDone { get; set; }


        public WarrantyOrderListViewModel()
        {
            // service
            _warrantyService = new WarrantyService();

            // data
            WaitForSent = true;
            Sent = true;
            WaitForCustomer = true;
            LoadData();

            // command
            ReloadData = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    LoadData();
                }
            );

            ChangeStatusToWaitForSent = new RelayCommand<object>
            (
                p => SelectedWarrantyOrder != null,
                p =>
                {
                    SelectedWarrantyOrder.WarrantyStatus = (int)WarrantyOrderStatus.WaitForSent;

                    // Save status into DB
                    _warrantyService.UpdateWarrantyOrderStatus(SelectedWarrantyOrder);

                    OnPropertyChanged(nameof(SelectedWarrantyOrder));
                    LoadData();
                }
            );

            ChangeStatusToSent = new RelayCommand<object>
            (
               p => SelectedWarrantyOrder != null,
               p =>
               {
                   SelectedWarrantyOrder.WarrantyStatus = (int)WarrantyOrderStatus.Sent;

                   // Save status into DB
                   _warrantyService.UpdateWarrantyOrderStatus(SelectedWarrantyOrder);
                   OnPropertyChanged(nameof(SelectedWarrantyOrder));
                   LoadData();
               }
            );
            ChangeStatusToWaitForCustomer = new RelayCommand<object>
            (
               p => SelectedWarrantyOrder != null,
               p =>
               {
                   SelectedWarrantyOrder.WarrantyStatus = (int)WarrantyOrderStatus.WaitForCustomer;

                   // Save status into DB
                   _warrantyService.UpdateWarrantyOrderStatus(SelectedWarrantyOrder);
                   OnPropertyChanged(nameof(SelectedWarrantyOrder));
                   LoadData();
               }
            );
            ChangeStatusToDone = new RelayCommand<object>
            (
                p => SelectedWarrantyOrder != null,
                p =>
                {
                    SelectedWarrantyOrder.WarrantyStatus = (int)WarrantyOrderStatus.Done;

                    // Save status into DB
                    _warrantyService.UpdateWarrantyOrderStatus(SelectedWarrantyOrder);

                    // Remove warranty order out of the warranty order list.
                    WarrantyOrders.Remove(SelectedWarrantyOrder);
                    OnPropertyChanged(nameof(SelectedWarrantyOrder));
                    LoadData();
                }
            );
        }

        private void LoadData()
        {

            var filter = new List<WarrantyOrderStatus>();
           
            if (WaitForSent) filter.Add(WarrantyOrderStatus.WaitForSent);
            if (Sent) filter.Add(WarrantyOrderStatus.Sent);
            if (WaitForCustomer) filter.Add(WarrantyOrderStatus.WaitForCustomer);

            var warrantyOrder = _warrantyService.GetWarrantyOrders(filter);
            WarrantyOrders = new ObservableCollection<ProductForListWarrantyDto>(warrantyOrder);


            NumberOfWaitForSent = warrantyOrder.Where(wo => wo.WarrantyStatus == (int)(WarrantyOrderStatus.WaitForSent)).Count();
            NumberOfSent = warrantyOrder.Where(wo => wo.WarrantyStatus == (int)(WarrantyOrderStatus.Sent)).Count();
            NumberOfWaitForCustomer = warrantyOrder.Where(wo => wo.WarrantyStatus == (int)(WarrantyOrderStatus.WaitForCustomer)).Count();
        }
    }
}
