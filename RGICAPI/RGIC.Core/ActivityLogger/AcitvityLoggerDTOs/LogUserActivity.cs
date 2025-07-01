using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.ActivityLogger.AcitvityLoggerDTOs
{
    public class LogUserActivity
    {
        public Guid? UserId { get; set; }
        public string? IpAddress { get; set; }
        public string? AreaAccessed { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string? Body { get; set; }
        public int? StatusCode { get; set; }
        public string? Method { get; set; }
        public string? Description { get; set; }
    }
}
