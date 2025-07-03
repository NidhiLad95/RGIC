using CRUDOperations;
using Dapper;
using Microsoft.Extensions.Configuration;
using RGIC.Core.Branch;
using RGIC.Core.Branch.BranchDTOs;
using RGIC.Core.Common;
using RGIC.Infrastructure;

namespace RGIC.Repositories
{
    public class BranchRepository(ICrudOperationService crudOperationService, IConfiguration configuration) : IBranchRepo
    {
        private readonly ICrudOperationService _crudOperationService = crudOperationService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<Response<DtoBranchMaster>> CreateBranch(DtoBranchMaster branch)
        {
            branch.CreatedBy = Guid.NewGuid().ToString();
            branch.CreatedOn = DateTime.UtcNow;

            return await _crudOperationService.InsertAndGet<DtoBranchMaster>("[SP_Branch_Create]", branch);
        }

        public async Task<Response<DtoBranchMaster>> UpdateBranch(DtoBranchMaster branch)
        {
            var data = await _crudOperationService.InsertUpdateDelete<DtoBranchMaster>("[SP_Branch_Update]", branch);

            return new Response<DtoBranchMaster>
            {
                Status = true, // or false if you check affected rows
                Message = "Branch updated successfully",
                Data = data
            };
        }


        public async Task<Response> DeleteBranch(int branchId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("BranchId", branchId);

            
            var rowsAffected = await _crudOperationService.Delete<string>("[SP_Branch_Delete]", parameters);

            return new Response
            {
                Status = true,
                Message = "Deleted successfully",
                Data = "BranchId: " + branchId.ToString()
            };
        }


        public async Task<Response<DtoBranchMaster>> GetBranchById(int branchId)
        {
            var parameters = new { BranchId = branchId };
            return await _crudOperationService.GetSingleRecord<DtoBranchMaster>("[SP_Branch_GetById]", parameters);
        }

        public async Task<ResponseGetList<DtoBranchMaster>> GetAllBranches() 
        {
            return await _crudOperationService.GetList<DtoBranchMaster>("[SP_Branch_GetAll]");
        }

    }
}
