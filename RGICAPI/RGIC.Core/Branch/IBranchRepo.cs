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
        
        Task<Response<DtoBranchMaster>> CreateBranch(DtoBranchvCreate branch);
        Task<Response<DtoBranchMaster>> UpdateBranch(DtoBranchUpdate branch);
        Task<Response<string>> DeleteBranch(DtoDeleteBranch DtoDelete);
        Task<Response<DtoBranchMaster>> GetBranchById(DtoGetBranchById dtoGetbyId);        
        Task<ResponseGetList<DtoBranchMaster>> GetAllBranches();
    }
}
