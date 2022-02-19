using SE214L22.Data.Entity.AppCustomer;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class InvoiceProductRepository : BaseRepository<InvoiceProduct>
    {
        public IEnumerable<InvoiceProduct> GetInvoiceProductsByCustomerPhoneNumber(string phoneNumber)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.InvoiceProducts
                    .Include(i => i.Invoice)
                    .Include(i => i.Invoice.Customer)
                    .Where(i => i.Invoice.Customer.PhoneNumber == phoneNumber)
                    .Include(i => i.Product)
                    .Include(i => i.Product.Manufacturer)
                    .Where(i => i.Product.WarrantyPeriod != null && DbFunctions.AddMonths(i.Invoice.CreationTime, (int)i.Product.WarrantyPeriod) >= DateTime.Now)
                    .OrderByDescending(i => i.Invoice.CreationTime);

                return query.ToList();
            }
        }

        public int GetNumberOfProductByInvoiceId(int invoiceId, int productId)
        {
            using (var ctx = new AppDbContext())
            {
                var invoiceProduct = ctx.InvoiceProducts
                    .Where(ip => ip.InvoiceId == invoiceId)
                    .Where(ip => ip.ProductId == productId)
                    .FirstOrDefault();

                return invoiceProduct != null ? invoiceProduct.Number : 0;
            }
        }
    }
}
