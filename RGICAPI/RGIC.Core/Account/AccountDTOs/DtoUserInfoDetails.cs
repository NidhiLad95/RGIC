using RGIC.Core.Common.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Account.AccountDTOs
{
    public class DtoUserInfoDetails
    {

        public Guid? UserId
        {
            get; set;
        }
        //[Required]
        //[EmailInput]
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
        public string? Email
        {
            get; set;
        }

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
        //[RegularExpression(@"^[a-zA-Z\-\']+$", ErrorMessage = "Last name should not contain numbers and special characters")]
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
        public string? LastName
        {
            get; set;
        }


        //[Required, DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Not a valid phone number. Phone Number must have a format as 000-000-0000.")]
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
        //public string? PhoneNumber
        //{
        //    get; set;
        //}

        //[DataType(DataType.Date)]
        //[Required, DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
        //public string? Dob
        //{
        //    get; set;
        //}

        //[Required, StringLength(12, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 1)]
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
        //public string? Gender
        //{
        //    get; set;
        //}

        public byte[]? PasswordHash
        {
            get; set;
        }
        public byte[]? PasswordSalt
        {
            get; set;
        }
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
        //public int RoleId
        //{
        //    get; set;
        //}
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
        //public string? Role
        //{
        //    get; set;
        //}
        
        public int Active
        {
            get; set;
        }

        public string? token
        {
            get; set;
        }

    }
}
