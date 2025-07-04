using RGIC.Core.Branch.BranchDTOs;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Product.ProductDTO
{
    public class GetAllProductDTO
    {
        public List<GetAllProductDTO> GetAllBranch { get; set; } = new List<GetAllProductDTO>();

    }
}



