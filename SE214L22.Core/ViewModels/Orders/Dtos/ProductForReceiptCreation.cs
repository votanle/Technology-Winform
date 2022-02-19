using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Orders.Dtos
{
    public class ProductForReceiptCreation : BaseDto
    {
        private int _number;
        private int _priceIn;

        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int PriceIn { get => _priceIn; set { _priceIn = value; OnPropertyChanged(); } }
        public int Number { get => _number; set { _number = value; OnPropertyChanged(); } }
    }
}
