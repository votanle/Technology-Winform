using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Sells.Dtos
{
    public class SelectingProductForSellDto : BaseDto
    {
        public int _selectedNumber;

        public int Id { get; set; }
        public string Name { get; set; }
        public int PriceOut { get; set; }
        public int SelectedNumber { get => _selectedNumber; set { _selectedNumber = value; OnPropertyChanged(); } }
    }
}
