using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Products.Dtos
{
    public class ProductForCreationDto : BaseDto
    {
        private string _name;
        private int _categoryId;
        private int _manufacturerId;
        private int _number;
        private int _priceIn;
        private int _warrantyPeriod;
        private float? _returnRate;
        private int _status;
        private string _photo;

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public int CategoryId { get => _categoryId; set { _categoryId = value; OnPropertyChanged(); } }
        public int ManufacturerId { get => _manufacturerId; set { _manufacturerId = value; OnPropertyChanged(); } }
        public int Number { get => _number; set { _number = value; OnPropertyChanged(); } }
        public int PriceIn { get => _priceIn; set { _priceIn = value; OnPropertyChanged(); } }
        public int WarrantyPeriod { get => _warrantyPeriod; set { _warrantyPeriod = value; OnPropertyChanged(); } }
        public float? ReturnRate { get => _returnRate; set { _returnRate = value; OnPropertyChanged(); } }
        public int Status { get => _status; set { _status = 0; OnPropertyChanged(); } }
        public string Photo { get => _photo; set { _photo = value; OnPropertyChanged(); } }

    }
}
