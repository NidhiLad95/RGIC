using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RGIC.Core.Common.CustomValidators
{
    public class EmailInputAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success!;

            var tagWithoutClosingRegex = new Regex(@"\A(?i)(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?-i)[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            var hasTags = tagWithoutClosingRegex.IsMatch(value.ToString()!);

            if (hasTags)
            {
                return ValidationResult.Success!;
            }
            else
            {
                return new ValidationResult(String.Format("The {0} is not a valid e-mail address.", value));
            }
        }
    }
}
