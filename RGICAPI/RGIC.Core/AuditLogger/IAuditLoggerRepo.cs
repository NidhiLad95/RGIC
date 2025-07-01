using CRUDOperations;
using RGIC.Core.AuditLogger.AuditLoggerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.AuditLogger
{
    public interface IAuditLoggerRepo
    {
        Task<Response> Log(DtoAuditLog auditLog);
        Task<Response> SaveAuditLog(DtoAuditLogger auditLogger);
    }
}
