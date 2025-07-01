using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RGIC.Core.Common.CustomValidators
{
    public class DenyXmlInputAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success!; // Null values are considered valid

            if (value is List<string> list)
            {
                // Check if ny of the strings in the list are XML
                foreach (string item in list)
                {
                    try
                    {
                        // Attempt to parse the input string as XML
                        XmlDocument xmlDoc = new();
                        xmlDoc.LoadXml(item);

                        // If parsing succeeds, the XML is considered invalid
                        return new ValidationResult(string.Format("XML tags not allowed in {0}", validationContext.DisplayName));
                    }
                    catch (XmlException)
                    {
                        // If parsing fails, the XML is considered valid
                        continue;
                    }
                }

                // If none of the strings in the list are XML, they are considered valid
                return ValidationResult.Success!;
            }
            else
            {
                string? xmlString = value as string;

                if (string.IsNullOrEmpty(xmlString))
                    return ValidationResult.Success!; // Empty strings are considered valid

                try
                {
                    // Attempt to parse the input string as XML
                    XmlDocument xmlDoc = new();
                    xmlDoc.LoadXml(xmlString);

                    // If parsing succeeds, the XML is considered valid
                    return new ValidationResult(string.Format("XML tags not allowed in {0}", validationContext.DisplayName));
                }
                catch (XmlException)
                {
                    // If parsing fails, the XML is considered invalid
                    return ValidationResult.Success!;
                }
            }
        }
    }
}

