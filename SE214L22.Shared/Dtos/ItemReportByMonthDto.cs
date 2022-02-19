using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Shared.Dtos
{
    public class ItemReportByMonthDto
    {
        public int Index { get; set; }
        public DateTime Day { get; set; }
        public int TotalRevenue { get; set; }
        public int TotalProfit { get; set; }
    }
}
