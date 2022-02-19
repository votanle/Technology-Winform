using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Seedings
{
    public class CustomerSeeder
    {
        private static List<Customer> Data
        {
            get
            {
                return new List<Customer>()
                {
                    new Customer
                    {
                        Id = 1,
                        Name = "Nguyễn Thanh Hoa",
                        PhoneNumber = "0123434565",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 2,
                        Name = "Nguyễn Xuân Tú",
                        PhoneNumber = "0123434365",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 3,
                        Name = "Lê Anh Tuấn",
                        PhoneNumber = "0723434365",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 4,
                        Name = "Lê Xuân Tùng",
                        PhoneNumber = "0723434366",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 5,
                        Name = "Nguyễn Thanh Tuấn",
                        PhoneNumber = "0723434266",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 6,
                        Name = "Vũ Duy Khách",
                        PhoneNumber = "0723134366",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 7,
                        Name = "Đào Xuân Tùns",
                        PhoneNumber = "0713434333",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 8,
                        Name = "Nguyễn Nhật Chiêu",
                        PhoneNumber = "0723404366",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 9,
                        Name = "Cao Thị Trang",
                        PhoneNumber = "0723334366",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 10,
                        Name = "Lưu Văn Bình",
                        PhoneNumber = "0721134366",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 11,
                        Name = "Nguyễn Nhật Chưởng",
                        PhoneNumber = "0723434996",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 12,
                        Name = "Võ Kim Sơn",
                        PhoneNumber = "0723224366",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 13,
                        Name = "Lê Xuân Tuấn",
                        PhoneNumber = "0723224366",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },
                    new Customer
                    {
                        Id = 14,
                        Name = "Lê Xuân Tiến",
                        PhoneNumber = "0723434366",
                        CustomerLevelId = 1,
                        AccumulatedPoint = 0,
                        CreationTime = DateTime.Now
                    },                    
                };
            }
        }
        public static void Seed(AppDbContext context)
        {
            foreach (var item in Data)
            {
                context.Customers.Add(item);
            }

            context.SaveChanges();
        }
    }
}
