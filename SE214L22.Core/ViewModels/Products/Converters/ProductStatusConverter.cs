using SE214L22.Data.Entity.AppProduct;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SE214L22.Core.ViewModels.Products
{
    public class ProductStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object paramater, CultureInfo culture)
        {
            try
            {
                if (value == null)
                    return "Không xác định";

                var productStatus = (ProductStatus)value;

                switch (productStatus)
                {
                    case ProductStatus.Available:
                        return "Đang kinh doanh";
                    case ProductStatus.Suspended:
                        return "Ngừng kinh doanh";
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
            throw new NotSupportedException("ProductStatusConverter can only be used for one way conversion.");
        }
    }
}
