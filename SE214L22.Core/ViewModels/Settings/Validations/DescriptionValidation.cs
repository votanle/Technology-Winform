using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SE214L22.Core.ViewModels.Settings
{
    public class DescriptionValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string description = (string)value;
            if (value == null || description.Trim().Length == 0)
            {
                return new ValidationResult(false, "Vui lòng nhập mô tả");
            }
            return ValidationResult.ValidResult;
        }
    }
}
