using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Branch.BranchDTOs
{
    public class DtoDeleteBranch
    {
        public int BranchID { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
