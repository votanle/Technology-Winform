using SE214L22.Data.Entity.AppProduct;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;
using SE214L22.Shared.Dtos;

namespace SE214L22.Data.Repository
{
    public class OrderRepository : BaseRepository<Order>
    {
        public IEnumerable<Order> GetOrders(List<OrderStatus> status, int limit)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.Orders.AsQueryable();

                if (status != null)
                {
                    query = query.Where(o => status.Contains((OrderStatus)o.Status));
                }
                query = query.Take(limit);

                query = query.Include(o => o.CreationUser).Include(o => o.Provider);
                var orders = query.ToList();

                return orders;
            }
        }

        public IEnumerable<Order> GetOrdersWithFilter(List<OrderStatus> status, int limit, DateRangeDto dateRange)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.Orders.AsQueryable();
                
                if (dateRange != null)
                {
                    query = query.Where(o => dateRange.StartDate <= o.CreationTime && o.CreationTime <= dateRange.EndDate);
                }

                if (status != null)
                {
                    query = query.Where(o => status.Contains((OrderStatus)o.Status));
                }
                query = query.Take(limit);

                query = query.Include(o => o.CreationUser).Include(o => o.Provider);
                var orders = query.ToList();

                return orders;
            }
        }

        public void UpdateOrderStatusById(int orderId, OrderStatus status)
        {
            using (var ctx = new AppDbContext())
            {
                var order = ctx.Orders.Where(o => o.Id == orderId).FirstOrDefault();
                order.Status = (int)status;
                ctx.SaveChanges();
            }
        }

        public void UpdateProviderById(int orderId, int providerId)
        {
            using (var ctx = new AppDbContext())
            {
                var order = ctx.Orders.Where(o => o.Id == orderId).FirstOrDefault();
                order.ProviderId = providerId;
                ctx.SaveChanges();
            }
        }
    }
}
