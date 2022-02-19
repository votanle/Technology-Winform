using SE214L22.Data.Entity.AppUser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SE214L22.Core.ViewModels.Users
{
    public class FeaturePermissionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object paramater, CultureInfo culture)
        {
            try
            {
                if (value == null) return "Visible";

                var grantedPermissions = (ObservableCollection<Permission>)value;
                var permission = (string)paramater;

                if (permission.Split(',').Length > 1)
                {
                    var permissions = permission.Split(',');
                    foreach (var pm in permissions)
                    {
                        if (grantedPermissions.Where(p => p.Name == pm).FirstOrDefault() != null)
                            return "Hidden";
                    }
                }

                if (grantedPermissions.Where(p => p.Name == permission).FirstOrDefault() == null)
                    return "Visible";

                return "Hidden";
            }
            catch (Exception)
            {
                return "Visible";
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("FeaturePermissionConverter can only be used for one way conversion.");
        }
    }
}
