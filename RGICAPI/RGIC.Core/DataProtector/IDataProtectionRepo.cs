using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.DataProtector
{
    public interface IDataProtectionRepo
    {
        string Encrypt(string input);

        string Decrypt(string cipherText);
    }
}
