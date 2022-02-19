using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Shared.Dtos
{
    public class ProductReportByDayDto
    {
        public int Index { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Number { get; set; }
        public int PriceOut { get; set; }
        public int Total { get; set; }
    }
}
