using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Infrastructure
{
    public class RGICException : Exception
    {
        public RGICException() : base("Something went wrong.") { }

        public RGICException(string message) : base(message) { }

        public RGICException(string message, Exception innerException) : base(message, innerException) { }
    
    }
}
