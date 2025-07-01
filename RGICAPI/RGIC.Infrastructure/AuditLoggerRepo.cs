using CRUDOperations;
using RGIC.Core.AuditLogger;
using RGIC.Core.AuditLogger.AuditLoggerDTOs;
using RGIC.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Infrastructure
{
    public class AuditLoggerRepo(ICrudOperationService crudOperationService,
        ICommonRepo commonRepo) :IAuditLoggerRepo
    {

        private ICrudOperationService _crudOperationService = crudOperationService;
        private readonly ICommonRepo _commonRepository = commonRepo;
        public async Task<Response> Log(DtoAuditLog auditLog)
        {

            var response = await _crudOperationService.InsertUpdateDelete<Response>("[Log].[UspSaveAuditLogger]", new
            {
                auditLog.ModuleName,
                auditLog.RowModificationIdentifier,
                auditLog.OldDataJson,
                auditLog.NewDataJson,
                auditLog.Reason,
                auditLog.Action,
                auditLog.IPAddress,
                auditLog.Controller,
                auditLog.ActionMethod,
                auditLog.CreatedBy

            }).ConfigureAwait(false);


            return response;
        }

        public async Task<Response> SaveAuditLog(DtoAuditLogger auditLogger)
        {
            return await _crudOperationService.InsertUpdateDelete<Response>("[Log].[UspSaveAuditLogger]", new
            {
                auditLogger.ModuleName,
                auditLogger.RowModificationIdentifier,
                auditLogger.OldDataJson,
                auditLogger.NewDataJson,
                auditLogger.Operation,
                auditLogger.IPAddress,
                auditLogger.Controller,
                auditLogger.ActionMethod,
                auditLogger.CreatedBy,
                auditLogger.Reason

            }).ConfigureAwait(false);
        }
    }
}
