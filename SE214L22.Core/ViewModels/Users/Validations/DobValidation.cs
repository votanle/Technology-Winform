using SE214L22.Core.Services.AppProduct;
using SE214L22.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SE214L22.Core.ViewModels.Users
{
    public class DobValidation : ValidationRule
    {
        private readonly ParameterService _parameterService;

        public DobValidation()
        {
            _parameterService = new ParameterService();
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                DateTime date = (DateTime)value;
                int minAgeAllowed = _parameterService.GetParameterByName(ParameterType.MinAge).Value;
                int maxAgeAllowed = _parameterService.GetParameterByName(ParameterType.MaxAge).Value;
                if (DateTime.Now.AddYears(-minAgeAllowed) < date        // date > lastestDate ~ age < minAge 
                    || date < DateTime.Now.AddYears(-maxAgeAllowed))    // date < oldestDate ~ age > maxAge
                {
                    return new ValidationResult(false, $"Độ tuổi cho phép là từ {minAgeAllowed} - {maxAgeAllowed}");
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Ngày sinh không hợp lệ");
            }

            return ValidationResult.ValidResult;
        }
    }
}
