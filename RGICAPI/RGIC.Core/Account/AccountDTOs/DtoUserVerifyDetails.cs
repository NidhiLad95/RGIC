using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Account.AccountDTOs
{
    public class DtoUserVerifyDetails
    {
        public Guid? UserId { get; set; }
        public string? EmailId { get; set; }

    }
}
