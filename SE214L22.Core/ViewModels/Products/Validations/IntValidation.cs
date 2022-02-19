using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SE214L22.Core.ViewModels.Products
{
    public class IntValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            float number = 0;

            try
            {
                if (((string)value).Length > 0)
                    number = Int32.Parse((String)value);
                else
                {
                    return new ValidationResult(false, "Vui lòng nhập!");
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Nội dung không hợp lệ");
            }

            if (number <= 0)
            {
                return new ValidationResult(false, "Nội dung phải là số dương");
            }
            return ValidationResult.ValidResult;
        }
    }
}
