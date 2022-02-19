using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class OrderProductRepository : BaseRepository<OrderProduct>
    {
        public IEnumerable<OrderProduct> GetOrderProductsByOrderId(int orderId)
        {
            using (var ctx = new AppDbContext())
            {
                return ctx.OrderProducts
                    .Where(op => op.OrderId == orderId)
                    .Include(op => op.Product)
                    .Include(op => op.Product.Category)
                    .ToList();
            }
        }

        public void DeleteAllByOrderId(int orderId)
        {
            using (var ctx = new AppDbContext())
            {
                var orderProducts = ctx.OrderProducts.Where(op => op.OrderId == orderId).ToList();
                ctx.OrderProducts.RemoveRange(orderProducts);
                ctx.SaveChanges();
            }
        }
    }
}
