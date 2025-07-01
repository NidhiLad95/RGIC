using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.AuditLogger.AuditLoggerDTOs
{
    public class DtoAuditLogger
    {
        public string? ModuleName { get; set; }
        public Guid? RowModificationIdentifier { get; set; }
        public string? OldDataJson { get; set; }
        public string? NewDataJson { get; set; }

        [
          Required(ErrorMessage = "Reason is required")
          , MaxLength(50, ErrorMessage = "Reason length should be less than or equal to 50 characters"), RegularExpression("^[A-Za-z0-9\'\\.\\-\\s\\,]*$", ErrorMessage = "Reason pattern is invalid")
      ]
        public string? Reason { get; set; }
        public string? Operation { get; set; }
        public string? IPAddress { get; set; }
        public string? Controller { get; set; }
        public string? ActionMethod { get; set; }
        public Guid? CreatedBy { get; set; }

    }
}
