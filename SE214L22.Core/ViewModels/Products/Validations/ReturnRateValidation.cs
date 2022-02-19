using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SE214L22.Core.ViewModels.Products
{
    public class ReturnRateValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            float returnRate = 0;

            try
            {
                if (((string)value).Length > 0)
                    returnRate = float.Parse((String)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Tỉ suất lợi nhuận không hợp lệ");
            }

            if (returnRate < 0)
            {
                return new ValidationResult(false, "Tỉ suất lợi nhuận phải là số dương");
            }
            return ValidationResult.ValidResult;
        }
    }
}
