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

        public async Task<Response<DtoProductMaster>> CreateProduct(DtoProductCreate objProduct)
        {

            var data = await _crudOperationService.InsertAndGet<string>("[UspProductCreate]", objProduct);
            return new Response<DtoProductMaster>
            {
                Status = data.Status,
                Message = data.Message,

                Data = new DtoProductMaster
                {
                    productID = Convert.ToInt32(data.Data),
                    CreatedBy = objProduct.CreatedBy,
                    FuelType = objProduct.FuelType,
                    NEPCalcType = objProduct.NEPCalcType,
                    ODTPFilter = objProduct.ODTPFilter,
                    ProductCategoryID = objProduct.ProductCategoryID,   
                    ProductClassID = objProduct.ProductClassID,
                    ProductGroupID = objProduct.ProductGroupID,
                    ProductSubCategory = objProduct.ProductSubCategory,
                    ProductSubclassID = objProduct.ProductSubclassID,
                    RenewalHealth =objProduct.RenewalHealth,
                    RenewalMotor   = objProduct.RenewalMotor
                    

                }
            };
        }

        public async Task<Response<DtoProductMaster>> UpdateProduct(DtoProductMaster objProduct)
        {
            var data = await _crudOperationService.InsertUpdateDelete<string>("[UspProductupdate]", objProduct);
            return new Response<DtoProductMaster>
            {
                Status = data.Status,
                Message = data.Message,

                Data=new DtoProductMaster
                {
                    productID = Convert.ToInt32(data.Data),
                    CreatedBy = objProduct.CreatedBy,
                    FuelType = objProduct.FuelType,
                    NEPCalcType = objProduct.NEPCalcType,
                    ODTPFilter = objProduct.ODTPFilter,
                    ProductCategoryID = objProduct.ProductCategoryID,
                    ProductClassID = objProduct.ProductClassID,
                    ProductGroupID = objProduct.ProductGroupID,
                    ProductSubCategory = objProduct.ProductSubCategory,
                    ProductSubclassID = objProduct.ProductSubclassID,
                    RenewalHealth = objProduct.RenewalHealth,
                    RenewalMotor = objProduct.RenewalMotor


                }
            };
        }


        public async Task<Response<string>> DeleteProduct(DtoProductDelete DtoDelete)
        {



            return await _crudOperationService.InsertUpdateDelete<string>("[UspProductDelete]", DtoDelete);

        }


        public async Task<Response<DtoProductMaster>> GetProductById(DtoGetProductById dtoGetbyId)
        {
            return await _crudOperationService.GetSingleRecord<DtoProductMaster>("[UspProductGetById]", dtoGetbyId);
        }

        public async Task<ResponseGetList<DtoProductMaster>> GetAllProducts()
        {
            return await _crudOperationService.GetList<DtoProductMaster>("[UspProductMasterGetAll]");
        }


    }
}
