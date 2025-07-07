using AuthLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RGIC.Core.Account.AccountDTOs;
using RGIC.Core.Branch;
using RGIC.Core.Branch.BranchDTOs;
using RGIC.Core.Common;
using RGIC.Core.Product;
using RGIC.Core.Product.ProductDTO;
using RGIC.Infrastructure;
using System.Configuration;

namespace RGICAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductRepo productRepo, IAuthLib authLib, IConfiguration configuration, ICommonRepo commonRepo) : BaseController
    {
        private readonly IProductRepo _productRepo = productRepo;
        private readonly ICommonRepo _CommonRepo = commonRepo;
        private readonly IAuthLib _authLib = authLib;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create([FromBody] DtoProductCreate productCreation)
        {
            //productCreation ??= new();
            if (!string.IsNullOrEmpty(User?.Identity?.Name))
            {
                productCreation.CreatedBy = new Guid(User.Identity.Name);
            }
            else
            {
                productCreation.CreatedBy = Guid.Empty;
            }
            //productCreation.CreatedBy = productCreation.CreatedBy;
            var response = await _productRepo.CreateProduct(productCreation).ConfigureAwait(false);

            if (response.Status)
            {
                return StatusCode(StatusCodes.Status201Created, response);
            }
            else if (!response.Status)
            {
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something went wrong, Please contact system administrator");
            }
        }
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update([FromBody] DtoProductMaster dtoProduct)
        {
            if (!string.IsNullOrEmpty(User?.Identity?.Name))
            {
                dtoProduct.UpdatedBy = new Guid(User.Identity.Name);
            }
            else
            {
                dtoProduct.UpdatedBy = Guid.Empty;
            }
            var response = await _productRepo.UpdateProduct(dtoProduct).ConfigureAwait(false);

            if (response.Status)
            {
                return StatusCode(StatusCodes.Status201Created, response);
            }
            else if (!response.Status)
            {
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something went wrong, Please contact system administrator");
            }
        }
        [HttpPost, Route("Delete")]
        public async Task<IActionResult> ProductCreate([FromBody] DtoProductDelete dtoDelete)
        {
            if (!string.IsNullOrEmpty(User?.Identity?.Name))
            {
                dtoDelete.UpdatedBy = new Guid(User.Identity.Name);
            }
            else
            {
                dtoDelete.UpdatedBy = Guid.Empty;
            }
            //productCreation.CreatedBy = productCreation.CreatedBy;
            var response = await _productRepo.DeleteProduct(dtoDelete).ConfigureAwait(false);

            if (response.Status)
            {
                return StatusCode(StatusCodes.Status201Created, response);
            }
            else if (!response.Status)
            {
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something went wrong, Please contact system administrator");
            }
        }
        [HttpPost, Route("GetBylId")]
        public async Task<IActionResult> GetById([FromBody] DtoGetProductById productById)
        {
            
            var response = await _productRepo.GetProductById(productById).ConfigureAwait(false);

            if (response.Status)
            {
                return StatusCode(StatusCodes.Status201Created, response);
            }
            else if (!response.Status)
            {
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something went wrong, Please contact system administrator");
            }
        }
        [HttpPost, Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productRepo.GetAllProducts().ConfigureAwait(false);

            if (response.Status)
            {
                return StatusCode(StatusCodes.Status201Created, response);
            }
            else if (!response.Status)
            {
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something went wrong, Please contact system administrator");
            }
        }



    }
}

