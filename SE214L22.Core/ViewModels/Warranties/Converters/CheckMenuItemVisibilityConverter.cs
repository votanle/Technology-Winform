using SE214L22.Core.ViewModels.Warranties.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SE214L22.Core.ViewModels.Warranties
{
    public class CheckMenuItemVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object paramater, CultureInfo culture)
        {
            try
            {
                if (value == null)
                    return "Hidden";
                var warrantyOrder = (ProductForListWarrantyDto)value;
                var status = (int)paramater;
                if (warrantyOrder.WarrantyStatus == status)
                    return "Visible";
                else
                    return "Hidden";
            }
            catch (Exception)
            {
                return "Hidden";
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("CheckMenuItemVisibilityConverter can only be used for one way conversion.");
        }
    }
}
