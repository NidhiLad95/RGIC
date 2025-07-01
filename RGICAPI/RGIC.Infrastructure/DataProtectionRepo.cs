using RGIC.Core.DataProtector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;

namespace RGIC.Infrastructure
{
    public class DataProtectionRepo : IDataProtectionRepo
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private const string Key = "bxl7NMlvyp7xORE45DqqvHXaB2sgI5lH";
        public DataProtectionRepo(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }
        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }
        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }
    }
}
