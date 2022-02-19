using SE214L22.Data.Entity.AppCustomer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SE214L22.Core.ViewModels.Warranties
{
    public class WarrantyOrderStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object paramater, CultureInfo culture)
        {
            try
            {
                if (value == null)
                    return "Không xác định";

                var warrantyOrderStatus = (WarrantyOrderStatus)value;

                switch (warrantyOrderStatus)
                {
                    case WarrantyOrderStatus.WaitForSent:
                        return "Đang chờ gửi";
                    case WarrantyOrderStatus.Sent:
                        return "Đã gửi bảo hành";
                    case WarrantyOrderStatus.WaitForCustomer:
                        return "Đang chờ khách hàng nhận";
                    case WarrantyOrderStatus.Done:
                        return "Bảo hành thành công";
                    default:
                        return "Không xác định";
                }
            }
            catch (Exception)
            {
                return "Không xác định";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("WarrantyListVisibilityConverter can only be used for one way conversion.");
        }
    }
}
