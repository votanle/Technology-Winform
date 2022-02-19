using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public IEnumerable<Category> GetCategories()
        {
            using (var ctx = new AppDbContext())
            {
                return ctx.Categories.ToList();
            }
        }
    }
}
