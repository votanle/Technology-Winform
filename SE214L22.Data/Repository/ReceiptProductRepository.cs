using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class ReceiptProductRepository : BaseRepository<ReceiptProduct>
    {
        public IEnumerable<ReceiptProduct> GetAllByReceiptId(int id)
        {
            using (var ctx = new AppDbContext())
            {
                return ctx.ReceiptProducts
                    .Where(rp => rp.ReceiptId == id)
                    .Include(rp => rp.Product)
                    .Include(rp => rp.Product.Category)
                    .ToList();
            }
        }
    }
}
