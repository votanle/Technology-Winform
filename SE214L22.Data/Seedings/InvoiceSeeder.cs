using SE214L22.Data.Entity.AppCustomer;
using System;

namespace SE214L22.Data.Seedings
{
    public class InvoiceSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Invoices.Add(new Invoice
            {
                Id = 1,
                CreationTime = DateTime.Now,
                UserId = 1,
                Price = 1_111_111,
                Discount = 0,
                Total = 1_111_111,
                CustomerId = 1,
            });

            context.InvoiceProducts.Add(new InvoiceProduct
            {
                Id = 1,
                InvoiceId = 1,
                Number = 2,
                ProductId = 1
            });


            context.SaveChanges();
        }
    }
}
