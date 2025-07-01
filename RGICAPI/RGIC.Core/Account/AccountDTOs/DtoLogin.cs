using RGIC.Core.Common.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Account.AccountDTOs
{
    public class DtoLogin
    {
        //[Required]
        //[EmailInputAttribute]
        //[DenyHtmlInput]
        //[DenyXmlInput]
        //[PreventXss]
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

        //public bool IsTermAndConditionAccepted { get; set; }

    }
}
