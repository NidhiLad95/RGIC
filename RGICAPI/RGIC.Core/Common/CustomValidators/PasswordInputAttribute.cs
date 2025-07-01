using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RGIC.Core.Common.CustomValidators
{
    public class PasswordInputAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success!;

            var tagWithoutClosingRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d.*)(?=.*\W.*)[a-zA-Z0-9\S]{8,}$");

            var hasTags = tagWithoutClosingRegex.IsMatch(value.ToString()!);

            if (hasTags)
            {
                return ValidationResult.Success!;
            }
            else
            {
                return new ValidationResult("Password must be 8 Characters long and must contain at least one upper case character, one lowercase character, one number and one special character.");
            }
        }
    }
}
