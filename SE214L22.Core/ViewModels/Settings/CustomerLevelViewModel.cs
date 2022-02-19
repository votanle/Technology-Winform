using SE214L22.Core.Services.AppCustomer;
using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.ViewModels.Settings.Dtos;
using SE214L22.Data.Entity.AppCustomer;
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
    public class CustomerLevelViewModel : BaseViewModel
    {
        // private service fields
        private readonly CustomerLevelService _customerLevelService;

        // private data fields
        private ObservableCollection<CustomerLevelForDisplayDto> _customerLevels;
        private CustomerLevelForDisplayDto _chosenCustomerLevel;


        // public data properties
        public ObservableCollection<CustomerLevelForDisplayDto> CustomerLevels
        {
            get => _customerLevels;
            set
            {
                _customerLevels = value;
                OnPropertyChanged();
            }
        }
        public CustomerLevelForDisplayDto ChosenCustomerLevel
        {
            get => _chosenCustomerLevel;
            set
            {
                _chosenCustomerLevel = value;
                OnPropertyChanged();
            }
        }


        // public command properties
        public ICommand UpdateCustomerLevel { get; set; }
        public ICommand PrepareUpdateCustomerLevel { get; set; }

        public CustomerLevelViewModel()
        {
            _customerLevelService = new CustomerLevelService();

            CustomerLevels = new ObservableCollection<CustomerLevelForDisplayDto>(_customerLevelService.GetDisplayCustomerLevels());

            PrepareUpdateCustomerLevel = new RelayCommand<object>
            (
                p => ChosenCustomerLevel == null ? false : true,
                p =>
                {
                }
             );
            UpdateCustomerLevel = new RelayCommand<object>
            (
                p => ChosenCustomerLevel == null ? false : true,
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        _customerLevelService.UpdateCustomerLevel(ChosenCustomerLevel);
                        CustomerLevels = new ObservableCollection<CustomerLevelForDisplayDto>(_customerLevelService.GetDisplayCustomerLevels());
                        MessageBox.Show("Cập nhật hạng khách hàng thành công!");
                    }
                }
             );
        }
    }
}
