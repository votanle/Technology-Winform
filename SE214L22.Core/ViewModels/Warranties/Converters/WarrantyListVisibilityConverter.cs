using SE214L22.Core.ViewModels.Warranties.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SE214L22.Core.ViewModels.Warranties
{
    public class WarrantyListVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object paramater, CultureInfo culture)
        {
            try
            {
                if (value == null)
                    return false;
             
                var customerProducts = (ObservableCollection<ProductForWarrantyDto>)(value);
                int noItems = customerProducts.Count;
                if (noItems == 0)
                {
                    return "Visible";
                }
                else
                {
                    return "Hidden";
                }
            }
            catch (Exception)
            {
                return "Hidden";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("WarrantyListVisibilityConverter can only be used for one way conversion.");
        }
    }
}
