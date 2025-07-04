using CRUDOperations;
using Microsoft.Extensions.Configuration;
using RGIC.Core.Branch;
using RGIC.Core.Branch.BranchDTOs;
using RGIC.Core.Product;
using RGIC.Core.Product.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RGIC.Repositories
{
    public class ProductRepository(ICrudOperationService crudOperationService, IConfiguration configuration) : IProductRepo
    {
        private readonly ICrudOperationService _crudOperationService = crudOperationService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<Response<string>> CreateProduct(DtoProductCreate objProduct)
        {
            objProduct.CreatedBy = Guid.NewGuid();
            

            return await _crudOperationService.InsertAndGet<string>("[UspProductCreate]", objProduct);
        }

    }
}
