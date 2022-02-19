using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Sells.Dtos
{
    public class InvoiceForCreationDto : BaseDto
    {
        private string _customerName;
        private string _phoneNumber;
        private int _total;
        private int _discount;
        private int _price;
        private string _customerLevel;


        public string CustomerName { get => _customerName; set { _customerName = value; OnPropertyChanged(); } }
        public string PhoneNumber { get => _phoneNumber; set { _phoneNumber = value; OnPropertyChanged(); } }
        public int Total { get => _total; set { _total = value; OnPropertyChanged(); } }
        public int Discount { get => _discount; set { _discount = value; OnPropertyChanged(); } }
        public int Price { get => _price; set { _price = value; OnPropertyChanged(); } }
        public string CustomerLevel { get => _customerLevel; set { _customerLevel = value; OnPropertyChanged(); } }

    }
}
