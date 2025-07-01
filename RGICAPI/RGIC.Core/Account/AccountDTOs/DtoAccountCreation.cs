using RGIC.Core.Common.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Account.AccountDTOs
{
    public class DtoAccountCreation
    {
        //[Required, StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        //[RegularExpression(@"^[a-zA-Z\-\']+$", ErrorMessage = "First name should not contain numbers and special characters")]
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
        public string? FirstName
        {
            get; set;
        }

        
        //[Required, StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        //[RegularExpression(@"^[a-zA-Z\-\']+$", ErrorMessage = "First name should not contain numbers and special characters")]
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
        public string? LastName
        {
            get; set;
        }

        //[Required]
        //[EmailInput]
        public string? Email
        {
            get; set;
        }


        //[Required, DataType(DataType.Password)]
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
        public string? Password
        {
            get; set;
        }

        public byte[]? PasswordHash
        {
            get; set;
        }
        public byte[]? PasswordSalt
        {
            get; set;
        }

        //public int? RoleID
        //{
        //    get; set;
        //}

        public Guid? CreatedBy
        {
            get; set;
        }
    }
}
