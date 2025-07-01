using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.AuditLogger.AuditLoggerDTOs
{
    public class DtoAuditLog
    {
        public string? ModuleName { get; set; }
        public Guid? RowModificationIdentifier { get; set; }

        public string? OldDataJson { get; set; }


        public string? NewDataJson { get; set; }
        // [Ignore]
        [MaxLength(200, ErrorMessage = "Reason length should be less than or equal to 200 characters")
          , RegularExpression("^[A-Za-z0-9\'\\.\\-\\s\\,]*$", ErrorMessage = "Reason pattern is invalid")
          ]

        public string? Reason { get; set; }
        public string? Action { get; set; }
        public string? IPAddress { get; set; }
        public string? Controller { get; set; }
        public string? ActionMethod { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool IsPushedToEntity { get; set; }
        public bool? IsEditsPushedToEntity { get; set; }
        public Guid? EntityId { get; set; }
        public bool IsConnectionRequired { get; set; } = true;

        //[Ignore]
        public string? Actor { get; set; }
    }

}
