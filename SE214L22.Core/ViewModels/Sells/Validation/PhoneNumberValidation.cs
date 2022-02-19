using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SE214L22.Core.ViewModels.Sells
{
    public class PhoneNumberValidation : ValidationRule
    {
        public PhoneNumberValidation()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || ((string)value).Trim().Length == 0)
                return new ValidationResult(false, "Vui lòng nhập số điện thoại");
            var parsedPhoneNumber = -1;
            if (!Int32.TryParse(value.ToString(), out parsedPhoneNumber))
            {
                return new ValidationResult(false, "Số điện thoại không hợp lệ");
            }
            return ValidationResult.ValidResult;
        }
    }
}
