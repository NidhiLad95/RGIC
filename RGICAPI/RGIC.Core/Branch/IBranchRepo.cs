using CRUDOperations;
using RGIC.Core.Branch.BranchDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Branch
{
    public interface IBranchRepo
    {
        //Task<Response<DtoBranchvCreate>> CreateBranch(DtoBranchvCreate branch);
        Task<Response<string>> CreateBranch(DtoBranchvCreate branch);
        Task<Response<DtoBranchMaster>> UpdateBranch(DtoBranchMaster branch);
        Task<Response> DeleteBranch(int branchId);
        Task<Response<DtoBranchMaster>> GetBranchById(int branchId);
        //Task<Response<DtoBranchGetAll>> GetAllBranches();

        Task<ResponseGetList<DtoBranchMaster>> GetAllBranches();
    }
}
