using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Settings.Dtos
{
    public class ManufacturerForCreationDto : BaseViewModel
    {
        private string _name;
        private string _description;

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(); } }
    }
}
