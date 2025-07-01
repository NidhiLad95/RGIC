using RGIC.Core.Common.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Account.AccountDTOs
{
    public class DtoValidateUserCredential
    {
        [Required]
        [EmailInput]
        public string? Email { get; set; }
        public byte[]? PasswordHash
        {
            get; set;
        }
        public byte[]? PasswordSalt
        {
            get; set;
        }
        [Required]
        public Guid? UserId { get; set; }

    }
}
