using SE214L22.Core.Services.AppUser;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace SE214L22.Core.ViewModels.Users
{
    public class EmailForUpdateValidationWrapper : DependencyObject
    {
        public static readonly DependencyProperty _userId =
             DependencyProperty.Register("UserId", typeof(int),
                typeof(EmailForUpdateValidationWrapper), new FrameworkPropertyMetadata());

        public int UserId
        {
            get { return (int)GetValue(_userId); }
            set { SetValue(_userId, value); }
        }
    }

    public class EmailForUpdateValidation : ValidationRule
    {
        private readonly UserService _userService;
        private readonly string EMAIL_PARTTERN = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

        public EmailForUpdateValidationWrapper Wrapper { get; set; }

        public EmailForUpdateValidation()
        {
            _userService = new UserService();
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string email = (string)value;
                // Check empty string?
                if (value == null || email.Trim().Length == 0)
                {
                    return new ValidationResult(false, "Vui lòng nhập email");
                }
                // No. character must be equal or greater than 10.
                if (email.Length < 10)
                {
                    return new ValidationResult(false, "Email phải có ít nhất 10 kí tự");
                }
                // Regular expression check email pattern
                if (!Regex.IsMatch(email, EMAIL_PARTTERN))
                {
                    return new ValidationResult(false, "Email chỉ được gồm các kí tự a-z, 0-9, _, -, @, .");
                }

                // Check if this email has been taken by other user.
                var userId = Wrapper.UserId;
                var user = _userService.GetUser((int)userId);

                if (user != null && email == user.Email)
                    return ValidationResult.ValidResult;

                if (_userService.EmailHasBeenTaken(email))
                    return new ValidationResult(false, "Email này đã được sử dụng rồi");

            }
            catch (Exception)
            {
                return new ValidationResult(false, "Email không hợp lệ");
            }
            return ValidationResult.ValidResult;
        }
    }
}
