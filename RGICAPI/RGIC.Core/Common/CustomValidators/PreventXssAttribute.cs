using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RGIC.Core.Common.CustomValidators
{
    public partial class PreventXssAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success!; // Null values are considered valid

            if (value is List<string> list)
            {
                foreach (string item in list)
                {
                    var xssPattern = MyRegexForXSS();

                    if (xssPattern.IsMatch(item))
                        return new ValidationResult(string.Format("Scripts not allowed in {0}", validationContext.DisplayName));
                }
            }
            else
            {
                string? inputString = value as string;

                if (string.IsNullOrEmpty(inputString))
                    return ValidationResult.Success!; // Empty strings are considered valid

                // Regular expression to match common XSS patterns
                var xssPattern = MyRegexForXSS();

                // Check if the input string contains any potential XSS patterns
                if (xssPattern.IsMatch(inputString))
                    return new ValidationResult(string.Format("Scripts not allowed in {0}", validationContext.DisplayName));
            }

            return ValidationResult.Success!;
        }

        [GeneratedRegex(@"<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>")]
        private static partial Regex MyRegexForXSS();
    }
    
}
[AttributeUsage(AttributeTargets.Property, Inherited = true)]
public class IgnoreAttribute : Attribute
{
}
