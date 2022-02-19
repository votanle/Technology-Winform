using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Shared.Dtos
{
    public class ProductFilterDto
    {
        public string NameProductKeyWord { get; set; }
        public List<string> ListCategory { get; set; }
        public List<string> ListManufacturer { get; set; }
    }
}
