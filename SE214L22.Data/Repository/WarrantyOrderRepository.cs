using SE214L22.Data.Entity.AppCustomer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class WarrantyOrderRepository : BaseRepository<WarrantyOrder>
    {
        public IEnumerable<WarrantyOrder> GetAllWithStatusFilter(List<WarrantyOrderStatus> filter)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.WarrantyOrders.AsQueryable();
                query = query.Where(wo => filter.Contains((WarrantyOrderStatus)(wo.Status)));
                query = query.Include(wo => wo.Customer);
                query = query.Include(wo => wo.Product);
                return query.ToList();
            }
        }

        public int GetNumberOfWarrantyOrderByInvoiceIdAndProductId(int invoiceId, int productId)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.WarrantyOrders
                    .Where(ip => ip.InvoiceId == invoiceId)
                    .Where(ip => ip.ProductId == productId);

                return query.Count();
            }
        }
    }
}
