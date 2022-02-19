using SE214L22.Core.Services.AppProduct;
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
    public class ReceiptViewModel : BaseViewModel
    {
        // service
        private readonly ReceiptService _receiptService;

        // private field
        private ObservableCollection<ReceiptForListDto> _receipts;
        private ReceiptForListDto _selectedReceipt;
        private ObservableCollection<ProductForReceiptCreation> _receiptProducts;
        private DateTime _dateFrom;
        private DateTime _dateTo;

        // public property
        public ObservableCollection<ReceiptForListDto> Receipts
        {
            get => _receipts;
            set
            {
                _receipts = value;
                OnPropertyChanged();
            }
        }

        public ReceiptForListDto SelectedReceipt
        {
            get => _selectedReceipt;
            set
            {
                _selectedReceipt = value;
                if (_selectedReceipt != null) InitializeReceiptProduct();
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

        // command
        public ICommand SearchWithFilter { get; set; }

        public ReceiptViewModel()
        {
            // serivce
            _receiptService = new ReceiptService();

            // data
            InitialData(null);
            DateFrom = DateTime.Now.AddMonths(-3);
            DateTo = DateTime.Now;

            // command
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
        }

        private void InitializeReceiptProduct()
        {
            ReceiptProducts = new ObservableCollection<ProductForReceiptCreation>(
                _receiptService.GetReceiptProducts(SelectedReceipt.Id));
        }

        private void InitialData(DateRangeDto dateRange)
        {
            Receipts = new ObservableCollection<ReceiptForListDto>(_receiptService.GetAllReceipts(dateRange));
            SelectedReceipt = null;

            ReceiptProducts = new ObservableCollection<ProductForReceiptCreation>();
        }
    }
}
