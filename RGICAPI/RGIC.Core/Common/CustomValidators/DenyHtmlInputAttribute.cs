using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RGIC.Core.Common.CustomValidators
{
    public partial class DenyHtmlInputAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success!;

            if (value is List<string> list)
            {
                foreach (string item in list)
                {
                    var tagWithoutClosingRegexItem = MyRegex();

                    var itemHasTags = tagWithoutClosingRegexItem.IsMatch(item);

                    if (!itemHasTags)
                        return ValidationResult.Success!;
                }
            }
            else
            {
                var tagWithoutClosingRegex = MyRegex();

                var hasTags = tagWithoutClosingRegex.IsMatch(value.ToString()!);

                if (!hasTags)
                    return ValidationResult.Success!;
            }

            return new ValidationResult(string.Format("HTML tags not allowed in {0}", validationContext.DisplayName));
        }

        [GeneratedRegex(@"<[^>]+>")]
        private static partial Regex MyRegex();

    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class IgnoreAttribute : Attribute
    {
    }
}
