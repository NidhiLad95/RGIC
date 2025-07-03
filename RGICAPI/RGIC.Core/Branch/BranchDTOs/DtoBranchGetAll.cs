using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Branch.BranchDTOs
{
    public class DtoBranchGetAll
    {
        //public DtoBranchGetAll() {
        //    this.GetAllBranch = new List<DtoBranchMaster>();
        //}
        public List<DtoBranchMaster> GetAllBranch { get; set; } = new List<DtoBranchMaster>();
    }
}
