using SE214L22.Data.Entity.AppProduct;
using System;

namespace SE214L22.Data.Seedings
{
    public class OrderSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Orders.Add(new Order
            {
                Id = 1,
                CreationTime = DateTime.Now,
                UserId = 1,
                ProviderId = 1,
                Status = (int)OrderStatus.WaitForSent,
            });
            context.OrderProducts.Add(new OrderProduct
            {
                Id = 1,
                Number = 10,
                OrderId = 1,
                ProductId = 1
            });
            context.Receipts.Add(new Receipt
            {
                Id = 1,
                OrderId = 1,
                UserId = 1,
                CreationTime = DateTime.Now,
                Total = 11_111_111
            });
            context.ReceiptProducts.Add(new ReceiptProduct
            {
                Id = 1,
                ReceiptId = 1,
                ProductId = 1,
                Number = 10,
                PriceIn = 1_111_111
            });

            context.SaveChanges();
        }
    }
}
