using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Settings.Dtos
{
    public class CustomerLevelForDisplayDto : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float PointLevel { get; set; }
        public float Discount { get; set; }
    }
}
