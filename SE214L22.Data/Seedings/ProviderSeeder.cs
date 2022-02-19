using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Seedings
{
    public class ProviderSeeder
    {
        private static List<Provider> Data
        {
            get
            {
                return new List<Provider>()
                {
                    new Provider
                    {
                        Id = 1,
                        Name = "Công ty TNHH Thiên Long",
                        Address = "TP Hồ Chí Minh",
                        Email =  "contact@thienlong.com",
                        PhoneNumber = "0378943593"
                    },
                    new Provider
                    {
                        Id = 2,
                        Name = "Công ty Kim Long",
                        Address = "TP Hà Nội",
                        Email =  "contact@kimlong.com",
                        PhoneNumber = "0288943593"
                    },
                    new Provider
                    {
                        Id = 3,
                        Name = "Cửa hàng Laptop 99",
                        Address = "TP Hồ Chí Minh",
                        Email =  "contact@laptop99.com",
                        PhoneNumber = "0368943593"
                    },
                    new Provider
                    {
                        Id = 4,
                        Name = "Máy tính Phong Vũ",
                        Address = "TP Đà Nẵng",
                        Email =  "contact@phongvucomputer.com",
                        PhoneNumber = "0379543593"
                    },
                    new Provider
                    {
                        Id = 5,
                        Name = "Công ty TNHH Nhất Thiên",
                        Address = "TP Hồ Chí Minh",
                        Email =  "contact@nhatthien.com",
                        PhoneNumber = "0124359349"
                    }
                };
            }
        }

        public static void Seed(AppDbContext context)
        {
            foreach (var item in Data)
            {
                context.Providers.Add(item);
            }
            context.SaveChanges();
        }
    }
}
