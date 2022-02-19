using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Home.Dtos
{
    public class ProcessingOrderDto
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatedUser { get; set; }
        public string ProviderName { get; set; }
        public string Status { get; set; }

        public static string MapEnumToStatus(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.WaitForSent:
                    return "Đang chờ gửi";
                case OrderStatus.Sent:
                    return "Đã gửi";
                case OrderStatus.Done:
                    return "Đã hoàn thành";
                default:
                    return "Không xác định";
            }
        }
    }
}
