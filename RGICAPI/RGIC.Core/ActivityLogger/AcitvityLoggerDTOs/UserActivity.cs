using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.ActivityLogger.AcitvityLoggerDTOs
{
    public class UserActivity
    {
        public Guid? UserId { get; set; }
        public string? RequestedFor { get; set; }

        public string? Message { get; set; }
    }
}
