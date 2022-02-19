using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SE214L22.Core.ViewModels.Settings
{
    public class DiscountValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            float returnRate = 0;

            try
            {
                if (((string)value).Length > 0)
                    returnRate = float.Parse((String)value);
                else
                {
                    return new ValidationResult(false, "Vui lòng nhập phần trăm giảm giá");
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Phần trăm giảm giá không hợp lệ");
            }

            if (returnRate < 0)
            {
                return new ValidationResult(false, "Phần trăm giảm giá phải là số dương");
            }
            return ValidationResult.ValidResult;
        }
    }
}
