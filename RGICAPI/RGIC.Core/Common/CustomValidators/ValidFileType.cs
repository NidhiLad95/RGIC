using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Common.CustomValidators
{
    public sealed class ValidFileType : ValidationAttribute
    {
        public string[]? AllowedExtensions { get; set; }

        public ValidFileType(params string[] allowedExtensions)
        {
            AllowedExtensions = allowedExtensions;
        }
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var list = value as IList;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var file = item as IFormFile;
                    var extension = "." + file!.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    var pos = Array.IndexOf(AllowedExtensions!, extension);
                    if (pos == -1)
                    {
                        return new ValidationResult($"'{file.FileName}' is not a valid file type. Only {String.Join(", ", AllowedExtensions!)} file types are allowed.");
                    }
                }
            }
            return ValidationResult.Success!;
        }
    }
}
