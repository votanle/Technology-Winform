using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SE214L22.Core.ViewModels.Settings
{
    public class AddressValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string address = (string)value;
                // Check empty string?
                if (value == null || address.Trim().Length == 0)
                {
                    return new ValidationResult(false, "Vui lòng nhập địa chỉ");
                }
                // No. character must be equal or greater than 6.
                if (address.Length < 6)
                {
                    return new ValidationResult(false, "Địa chỉ phải có ít nhất 6 kí tự");
                }

            }
            catch (Exception)
            {
                return new ValidationResult(false, "Địa chỉ không hợp lệ");
            }
            return ValidationResult.ValidResult;
        }
    }
}
