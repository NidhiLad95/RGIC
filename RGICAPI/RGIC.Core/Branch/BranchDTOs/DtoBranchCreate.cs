using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Branch.BranchDTOs
{
    public class DtoBranchvCreate
    {
        public int RegionMarket_Type_ID { get; set; }
        public string? Branch { get; set; }
        public string? BranchType { get; set; }
        public string? Location { get; set; }
        public string? BranchStatus { get; set; }
        public string? Zone { get; set; }
        public string? NewZone { get; set; }
        public string? State { get; set; }
        public string? RegionCity { get; set; }
        public string? BranchTP { get; set; }
        public string? MarketType { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        

    }
}
