using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Orders.Dtos
{
    public class ReceiptForListDto
    {
        public int Id { get; set; }
        public string CreationTime { get; set; }
        public string CreationUser { get; set; }
        public string ProviderName { get; set; }
        public int Total { get; set; }
    }
}
