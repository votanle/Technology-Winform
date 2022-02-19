using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Products.Dtos
{
    public class ProductDisplayDto : BaseDto
    {
        private int _status;
        private float? _returnRate;
        private string _photo;


        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public int Number { get; set;  }
        public int PriceIn { get; set; }
        public int PriceOut { get; set; }
        public int WarrantyPeriod { get; set; }
        public float? ReturnRate { get=> _returnRate; set { _returnRate = value; OnPropertyChanged(); } }
        public int Status { get => _status; set { _status = value; OnPropertyChanged(); } }
        public string Photo { get => _photo; set { _photo = value; OnPropertyChanged(); Console.WriteLine("photo", _photo); } }
        public Category Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string CheckReturnRateChange { get; set; }

        public static string MapEnumToStatus(ProductStatus status)
        {
            switch (status)
            {
                case ProductStatus.Available:
                    return "Đang kinh doanh";
                case ProductStatus.Suspended:
                    return "Ngừng kinh doanh";
                default:
                    return "Không xác định";
            }
        }

    }
}
