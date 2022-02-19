using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class CustomerLevelRepository : BaseRepository<CustomerLevel>
    {
        public IEnumerable<CustomerLevel> GetCustomerLevels()
        {
            using (var ctx = new AppDbContext())
            {
                return ctx.CustomerLevels.ToList();
            }
        }

        public CustomerLevel GetCustomerLevelByName(string name)
        {
            using (var ctx = new AppDbContext())
            {
                return ctx.CustomerLevels
                    .Where(u => u.Name == name)
                    .FirstOrDefault();
            }
        }
    }
}
