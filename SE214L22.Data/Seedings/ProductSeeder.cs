using SE214L22.Data.Entity.AppProduct;
using SE214L22.Shared.AppConsts;
using SE214L22.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Seedings
{
    public class ProductSeeder
    {
        private static List<Product> Data
        {
            get
            {
                return new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Macbook Pro 2015",
                        ManufacturerId = 1,
                        CategoryId = 1,
                        PriceIn = 24500000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Macbook Pro 2016",
                        ManufacturerId = 1,
                        CategoryId = 1,
                        PriceIn = 26500000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Macbook Pro 2017",
                        ManufacturerId = 1,
                        CategoryId = 1,
                        PriceIn = 28500000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 4,
                        Name = "Dell Inspiron 5590",
                        ManufacturerId = 6,
                        CategoryId = 1,
                        PriceIn = 30250000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 5,
                        Name = "Dell Inspiron 4590",
                        ManufacturerId = 6,
                        CategoryId = 1,
                        PriceIn = 29900000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 6,
                        Name = "Dell Inspiron 2590",
                        ManufacturerId = 6,
                        CategoryId = 1,
                        PriceIn = 19500000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 7,
                        Name = "Asus Nitro 5",
                        ManufacturerId = 3,
                        CategoryId = 1,
                        PriceIn = 21900000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 8,
                        Name = "Tai nghe Sony CH510",
                        ManufacturerId = 2,
                        CategoryId = 4,
                        PriceIn = 890000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 9,
                        Name = "Loa Soundmax A-89/4.1",
                        ManufacturerId = 7,
                        CategoryId = 5,
                        PriceIn = 2100000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 10,
                        Name = "Chuột Logitech M331",
                        ManufacturerId = 8,
                        CategoryId = 2,
                        PriceIn = 290000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 11,
                        Name = "Ổ cứng HDD Kingston A400",
                        ManufacturerId = 4,
                        CategoryId = 6,
                        PriceIn = 960000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 12,
                        Name = "Ổ cứng SSD Kingston A200",
                        ManufacturerId = 4,
                        CategoryId = 6,
                        PriceIn = 1520000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 13,
                        Name = "Màn hình Samsung LS24R3",
                        ManufacturerId = 9,
                        CategoryId = 7,
                        PriceIn = 4510000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 14,
                        Name = "Màn hình Samsung LRX5R3",
                        ManufacturerId = 9,
                        CategoryId = 7,
                        PriceIn = 3200000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 15,
                        Name = "Bàn phím Asus TUF K5",
                        ManufacturerId = 3,
                        CategoryId = 3,
                        PriceIn = 890000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 16,
                        Name = "Bàn phím Asus TUD A0",
                        ManufacturerId = 3,
                        CategoryId = 3,
                        PriceIn = 720000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 17,
                        Name = "Bàn phím Logitech MK20",
                        ManufacturerId = 8,
                        CategoryId = 3,
                        PriceIn = 1240000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 18,
                        Name = "Bàn phím Logitech M204",
                        ManufacturerId = 8,
                        CategoryId = 3,
                        PriceIn = 920000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 19,
                        Name = "Chuột Không dây Dell WM206",
                        ManufacturerId = 6,
                        CategoryId = 2,
                        PriceIn = 620000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                    new Product
                    {
                        Id = 20,
                        Name = "Chuột Dell MHK634",
                        ManufacturerId = 6,
                        CategoryId = 2,
                        PriceIn = 480000,
                        Status = (int)ProductStatus.Available,
                        WarrantyPeriod = 24,
                        ReturnRate = 10,
                        isDelete = 0,
                        Number = 10,
                        Photo = DefaultPhotoNames.Product
                    },
                };
            }
        }

        public static void Seed(AppDbContext context)
        {
            foreach (var item in Data)
            {
                item.PriceOut = Helper.CalculatePriceout(item.PriceIn, (float)item.ReturnRate);
                context.Products.Add(item);
            }
            context.SaveChanges();
        }
    }
}
