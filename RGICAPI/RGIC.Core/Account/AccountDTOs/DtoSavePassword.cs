using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Account.AccountDTOs
{
    
    public class DtoSavePassword
    {
        public string? UserId
        {
            get; set;
        }
        public byte[]? OldPasswordHash
        {
            get; set;
        }
        public byte[]? OldPasswordSalt
        {
            get; set;
        }
        public byte[]? NewPasswordHash
        {
            get; set;
        }
        public byte[]? NewPasswordSalt
        {
            get; set;
        }
    }

}
