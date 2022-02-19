using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Warranties.Dtos
{
    public class ProductForWarrantyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }
        public DateTime InvoiceTime { get; set; }
        public int WarrantyTimeRemaining { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }

        public int CustomerId { get; set; }
        public int InvoiceId { get; set; }

        public static int CalcWarrantyMonthRemaining(DateTime start, int WarrantyPeriod) // month
        {
            var period = start.AddMonths(WarrantyPeriod) - DateTime.Now;
            return (int)Math.Floor(period.TotalDays / 30.0);
        }
    }
}
