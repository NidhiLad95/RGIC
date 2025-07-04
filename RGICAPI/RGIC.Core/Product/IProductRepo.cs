using CRUDOperations;
using RGIC.Core.Branch.BranchDTOs;
using RGIC.Core.Product.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Product
{
    public interface IProductRepo
    {
        Task<Response<string>> CreateProduct(DtoProductCreate objProduct);
    }
}
