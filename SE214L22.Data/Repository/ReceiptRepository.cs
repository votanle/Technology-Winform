using SE214L22.Data.Entity.AppProduct;
using SE214L22.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class ReceiptRepository : BaseRepository<Receipt>
    {
        public IEnumerable<Receipt> GetAll(DateRangeDto dateRange)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.Receipts.AsQueryable();
                if (dateRange != null)
                {
                    dateRange.EndDate.AddDays(1);
                    query = query.Where(rc => dateRange.StartDate <= rc.CreationTime && rc.CreationTime <= dateRange.EndDate);
                }
                return query
                    .Include(rc => rc.Order)
                    .Include(rc => rc.Order.Provider)
                    .Include(rc => rc.CreationUser)
                    .ToList();
            }
        }
    }
}
