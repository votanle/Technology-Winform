using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SE214L22.Core.ViewModels.Users
{
    public class ComfirmPasswordValidationWrapper : DependencyObject
    {
        public static readonly DependencyProperty comparePasswordProperty =
             DependencyProperty.Register("ComparePassword", typeof(string),
                typeof(ComfirmPasswordValidationWrapper), new FrameworkPropertyMetadata());

        public string ComparePassword
        {
            get { return (string)GetValue(comparePasswordProperty); }
            set { SetValue(comparePasswordProperty, value); }
        }
    }

    public class ComfirmPasswordValidation : ValidationRule
    {
        public ComfirmPasswordValidationWrapper Wrapper { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var password = Wrapper.ComparePassword;
            var comfirmPassword = (string)value;
            if (comfirmPassword == "")
            {
                return new ValidationResult(false, "Vui lòng xác nhận mật khẩu");
            }
            if (password != null && !password.Equals(comfirmPassword))
            {
                return new ValidationResult(false, "Mật khẩu xác nhận không trùng khớp");
            }
            return ValidationResult.ValidResult;
        }
    }
}
