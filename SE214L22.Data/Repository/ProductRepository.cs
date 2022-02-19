using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Repository.AggregateDto;
using SE214L22.Shared.Dtos;
using SE214L22.Shared.Helpers;
using SE214L22.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class ProductRepository : BaseRepository<Product>
    {
        public IEnumerable<ProductAggregateDto> GetProductsOrderBySales(DateTime day, int limit = 10)
        {
            using (var ctx = new AppDbContext())
            {
                // group all alike productid and sum up sales no
                var hotProductIds = ctx.InvoiceProducts
                    .Include(p => p.Invoice)
                    .Where(p => p.Invoice.CreationTime.Month == day.Month && p.Invoice.CreationTime.Year == day.Year)
                    .GroupBy(p => p.ProductId)
                    .Select(p => new { ProductId = p.Key, SalesNo = p.Sum(i => i.Number) })
                    .OrderByDescending(r => r.SalesNo)
                    .Take(limit)
                    .ToList();

                // get all ids
                var Ids = hotProductIds.Select(x => x.ProductId).ToList();

                // get all products with those ids
                var list = ctx.Products
                    .Where(p => Ids.Contains(p.Id))
                    .Include(p => p.Manufacturer)
                    .ToList();

                // return data with sales no
                List<ProductAggregateDto> dataForReturn = new List<ProductAggregateDto>();
                foreach(var product in list)
                {
                    var salesNo = hotProductIds.Where(x => x.ProductId == product.Id).FirstOrDefault().SalesNo;
                    dataForReturn.Add(new ProductAggregateDto { Product = product, SalesNo = salesNo });
                }

                return dataForReturn.OrderByDescending(x => x.SalesNo).ToList();
            }
        }

        public void UpdateSaleProperty(int id, int number, int priceIn)
        {
            using (var ctx = new AppDbContext())
            {
                var product = ctx.Products.Include(p => p.Category).Where(p => p.Id == id).FirstOrDefault();
                product.Number += number;
                product.PriceIn = priceIn;
                // recalculate the priceOut
                if (product.ReturnRate == null)
                    product.PriceOut = Helper.CalculatePriceout(product.PriceIn, product.Category.ReturnRate);
                else
                    product.PriceOut = Helper.CalculatePriceout(product.PriceIn, (float)product.ReturnRate);
                ctx.SaveChanges();
            }
        }

       public PaginatedList<Product> GetProducts( int page, int limit, ProductFilterDto Filter = null)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.Products.AsQueryable();
                query = query.Where(p => p.isDelete == 0);
                if(Filter!=null)
                {
                    if (Filter.NameProductKeyWord != null && Filter.NameProductKeyWord != "")
                    {
                        query = query.Where(p => p.Name.ToLower().Contains((Filter.NameProductKeyWord).ToLower()));
                    }
                    if (Filter.ListCategory != null)
                    {
                        query = query.Where(p => Filter.ListCategory.Contains(p.Category.Name));
                    }
                    if (Filter.ListManufacturer != null)
                    {
                        query = query.Where(p => Filter.ListManufacturer.Contains(p.Manufacturer.Name));
                    }
                }                

                query = query.Include(p => p.Category)
                    .Include(p => p.Manufacturer)
                    .OrderBy(p => p.Name);

                var products = PaginatedList<Product>.Create(query, page, limit);
                
                return products;
            }
        }

        public PaginatedList<Product> GetProductsForImport(string keyword, int page, int limit, bool filterEmpty = false)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ctx.Products.AsQueryable();

                if (filterEmpty)
                    query = query.Where(p => p.Number > 0);

                if (keyword != null && keyword.ToLower().Trim() != "")
                    query = query.Where(p => p.Name.ToLower().Contains(keyword.ToLower().Trim()));

                query = query.Include(p => p.Category)
                    .Include(p => p.Manufacturer)
                    .OrderBy(p => p.Name);

                var products = PaginatedList<Product>.Create(query, page, limit);

                return products;
            }
        }

        public void UpdateNumberById(int id, int selectedNumber)
        {
            using (var ctx = new AppDbContext())
            {
                var product = ctx.Products.Where(p => p.Id == id).FirstOrDefault();
                if (product != null)
                    product.Number -= selectedNumber;
                ctx.SaveChanges();
            }
        }

        public string GetProductPhotoById(int id)
        {
            using (var ctx = new AppDbContext())
            {
                return ctx.Products.Where(x => x.Id == id).Select(x => x.Photo).FirstOrDefault();
            }
        }
    }
}
