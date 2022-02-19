using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Sells.Dtos
{
    public class ProductForSellDto  : BaseDto
    {
        private int _number { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
        public int Number { get => _number; set { _number = value; OnPropertyChanged(); } }
        public int PriceOut { get; set; }


    }
}
