using SE214L22.Data.Entity.AppCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Warranties.Dtos
{
    public class ProductForListWarrantyDto : BaseDto
    {
        private int _warrantyStatus { get; set; }

        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int WarrantyStatus { get => _warrantyStatus; set { _warrantyStatus = value; OnPropertyChanged(); } }
    }
}
