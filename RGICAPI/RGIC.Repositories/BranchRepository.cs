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

        public async Task<Response<DtoBranchMaster>> CreateBranch(DtoBranchvCreate branch)
        {
            
           var data=     await _crudOperationService.InsertAndGet<string>("[SP_Branch_Create]", branch);
            return new Response<DtoBranchMaster>
            { Status = data.Status, Message = data.Message,

                Data = new DtoBranchMaster
                {
                    BranchID = Convert.ToInt32(data.Data),
                    Branch = branch.Branch,
                    BranchStatus = branch.BranchStatus,
                    BranchTP = branch.BranchTP,
                    BranchType = branch.BranchType,
                    Location = branch.Location,
                    CreatedBy = branch.CreatedBy,
                    CreatedOn = branch.CreatedOn,
                    MarketType = branch.MarketType,
                    NewZone = Convert.ToString(branch.NewZone),
                    RegionCity = Convert.ToString(branch.RegionCity),
                    RegionMarket_Type_ID = branch.RegionMarket_Type_ID,
                    State = branch.State,
                    Zone = branch.Zone

                }
            };           
        }

        public async Task<Response<DtoBranchMaster>> UpdateBranch(DtoBranchUpdate branch)
        {
            var data = await _crudOperationService.InsertUpdateDelete<string>("[SP_Branch_Update]", branch);
            return new Response<DtoBranchMaster>
            {
                Status = data.Status,
                Message = data.Message,

                Data = new DtoBranchMaster
                {
                    BranchID = Convert.ToInt32(data.Data),
                    Branch = branch.Branch,
                    BranchStatus = branch.BranchStatus,
                    BranchTP = branch.BranchTP,
                    BranchType = branch.BranchType,
                    Location = branch.Location,
                    MarketType = branch.MarketType,
                    NewZone = Convert.ToString(branch.NewZone),
                    RegionCity = Convert.ToString(branch.RegionCity),
                    RegionMarket_Type_ID = branch.RegionMarket_Type_ID,
                    State = branch.State,
                    Zone = branch.Zone

                }
            };
        }


        public async Task<Response<string>> DeleteBranch(DtoDeleteBranch DtoDelete)
        {
            

            
            return await _crudOperationService.InsertUpdateDelete<string>("[SP_Branch_Delete]", DtoDelete);

        }


        public async Task<Response<DtoBranchMaster>> GetBranchById(DtoGetBranchById dtoGetbyId)
        {
            return await _crudOperationService.GetSingleRecord<DtoBranchMaster>("[SP_Branch_GetById]", dtoGetbyId);
        }

        public async Task<ResponseGetList<DtoBranchMaster>> GetAllBranches() 
        {
            return await _crudOperationService.GetList<DtoBranchMaster>("[SP_Branch_GetAll]");
        }

    }
}
