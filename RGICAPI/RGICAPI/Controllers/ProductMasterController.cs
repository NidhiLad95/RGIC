using AuthLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RGIC.Core.Account.AccountDTOs;
using RGIC.Core.Branch;
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
    public class ProductMasterController(IProductRepo productRepo, IAuthLib authLib, IConfiguration configuration, ICommonRepo commonRepo) : BaseController
    {
        private readonly IProductRepo _productRepo = productRepo;
        private readonly ICommonRepo _CommonRepo = commonRepo;
        private readonly IAuthLib _authLib = authLib;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost, Route("ProductCreate")]
        public async Task<IActionResult> ProductCreate([FromBody] DtoProductCreate? productCreation)
        {
            productCreation ??= new();
            if (!string.IsNullOrEmpty(User?.Identity?.Name))
            {
                productCreation.CreatedBy = new Guid(User.Identity.Name);
            }
            else
            {
                productCreation.CreatedBy = Guid.Empty;
            }
            productCreation.CreatedBy = productCreation.CreatedBy;
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

    }
}

