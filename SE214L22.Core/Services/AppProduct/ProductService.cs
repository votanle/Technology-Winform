using SE214L22.Core.ViewModels.Home.Dtos;
using SE214L22.Core.ViewModels.Orders.Dtos;
using SE214L22.Core.ViewModels.Products.Dtos;
using SE214L22.Core.ViewModels.Sells.Dtos;
using SE214L22.Data;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Repository;
using SE214L22.Shared.AppConsts;
using SE214L22.Shared.Dtos;
using SE214L22.Shared.Helpers;
using SE214L22.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.Services.AppProduct
{
    public class ProductService : BaseService
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
            _categoryRepository = new CategoryRepository();
        }

        /// <summary>
        /// Get list of hot products in a period of time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="type">Day or Month or Year?</param>
        /// <returns></returns>
        public IEnumerable<HotProductDto> GetHotProducts(DateTime dateTime, TimeType type = TimeType.Month)
        {
            if (type == TimeType.Month)
            {
                return Mapper.Map<IEnumerable<HotProductDto>>(_productRepository.GetProductsOrderBySales(dateTime));
            }

            return null;
        }

        public PaginatedList<ProductForOrderCreationDto> GetProductsForOrderCreation(string keyword, int? page, int? limit)
        {
            if (page == null || page == 0) page = 1;
            if (limit == null) limit = 17;
            var rawProducts = _productRepository.GetProductsForImport(keyword, (int)page, (int)limit);

            var productsForReturn = new PaginatedList<ProductForOrderCreationDto>
            (
                Mapper.Map<List<ProductForOrderCreationDto>>(rawProducts.Data),
                rawProducts.TotalRecords,
                rawProducts.CurrentPage,
                rawProducts.PageRecords
            );
            return productsForReturn;
        }

        public PaginatedList<ProductForSellDto> GetProductsForSell(string keyword, int? page, int? limit)
        {
            if (page == null || page == 0) page = 1;
            if (limit == null) limit = 16;
            var rawProducts = _productRepository.GetProductsForImport(keyword, (int)page, (int)limit, true);

            var productsForReturn = new PaginatedList<ProductForSellDto>
            (
                Mapper.Map<List<ProductForSellDto>>(rawProducts.Data),
                rawProducts.TotalRecords,
                rawProducts.CurrentPage,
                rawProducts.PageRecords
            );
            return productsForReturn;
        }

        public PaginatedList<ProductDisplayDto> GetProductsForDisplayProduct(int page = 1, int limit = 9, ProductFilterDto Filter = null)
        {
            var rawProducts = _productRepository.GetProducts( page, limit, Filter);

            var productsForReturn = new PaginatedList<ProductDisplayDto>
            (
                Mapper.Map<List<ProductDisplayDto>>(rawProducts.Data),
                rawProducts.TotalRecords,
                rawProducts.CurrentPage,
                rawProducts.PageRecords
            );
            return productsForReturn;
        }

        public Product AddProduct(ProductForCreationDto product)
        {
            // copy to save photo
            string newName = GetImageName();
            string desFile = GetFullPath(newName);
            try
            {
                File.Copy(product.Photo, desFile, true);
            }
            catch
            {
                System.Windows.MessageBox.Show("Đã xảy ra lỗi khi lưu file!");
                return null;
            }

            // save products
            var newProduct = Mapper.Map<Product>(product);
            product.Photo = newName;
            if (newProduct.ReturnRate != null)
                newProduct.PriceOut = Helper.CalculatePriceout(newProduct.PriceIn, (float)newProduct.ReturnRate);
            else
            {
                var category = _categoryRepository.Get(product.CategoryId);
                newProduct.PriceOut = Helper.CalculatePriceout(newProduct.PriceIn, (float)category.ReturnRate);
            }   

            return _productRepository.Create(newProduct);
        }

        public bool UpdateProduct(ProductDisplayDto product)
        {
            var editProduct = Mapper.Map<Product>(product);

            var oldPhotoName = _productRepository.GetProductPhotoById(product.Id);
            // check if there is a photo change?
            if (product.Photo != GetFullPath(oldPhotoName))
            {
                // delete old photo
                if (oldPhotoName != DefaultPhotoNames.Product)
                {
                    try
                    {
                        File.Delete(GetFullPath(oldPhotoName));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error");
                        Console.WriteLine(e.Message);
                    }
                }

                string newName = GetImageName();
                // copy to save as new photo
                string desFile = GetFullPath(newName);
                try
                {
                    File.Copy(product.Photo, desFile, true);
                    editProduct.Photo = newName;
                }
                catch
                {
                    System.Windows.MessageBox.Show("Đã xảy ra lỗi khi lưu file!");
                    return false;
                }
            }
            else
            {
                editProduct.Photo = oldPhotoName;
            }

            if (product.CheckReturnRateChange != "changed")
            {
                product.PriceOut = Helper.CalculatePriceout(product.PriceIn, (float)product.ReturnRate);                
            }
            else
            {   
                product.ReturnRate = null;
                product.PriceOut = Helper.CalculatePriceout(product.PriceIn, product.Category.ReturnRate);
            }

            return _productRepository.Update(editProduct);
        }
        public bool HidenProduct(ProductDisplayDto product)
        {
            var hidenProduct = Mapper.Map<Product>(product);
            hidenProduct.isDelete = 1;
            return _productRepository.Update(hidenProduct);
        }

        public bool DeleteProduct(Product product)
        {
            var oldPhotoName = _productRepository.GetProductPhotoById(product.Id);
            if (oldPhotoName != DefaultPhotoNames.Product)
            {
                try
                {
                    File.Delete(GetFullPath(oldPhotoName));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error");
                    Console.WriteLine(e.Message);
                }
            }

            return _productRepository.Delete(product.Id);
        }

        private string GetFullPath(string fileName)
        {
            string destPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string destinationFile = Path.Combine(destPath, "Photos", "Products", fileName);
            return destinationFile;
        }

        private string GetImageName()
        {
            var now = DateTime.Now.ToString("HHmmss_ddMMyyyy");
            return $"product_{now}.jpg";
        }
    }
}
