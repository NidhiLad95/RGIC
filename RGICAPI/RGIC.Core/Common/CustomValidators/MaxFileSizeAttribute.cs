using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Common.CustomValidators
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        public int MaxSize { get; }

        public MaxFileSizeAttribute(int fileSize)
        {
            MaxSize = fileSize;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return new ValidationResult("Invalid input type. Expected an IFormFile.");
            }

            if (file.Length > MaxSize)
            {
                return new ValidationResult($"'{file.FileName}' should be less than {MaxSize / 1024 / 1024}MB.");
            }

            return ValidationResult.Success!;
        }
    }
}
