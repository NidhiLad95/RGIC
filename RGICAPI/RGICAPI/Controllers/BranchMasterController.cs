using AuthLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RGIC.Core.Branch;
using RGIC.Core.Branch.BranchDTOs;
using RGIC.Core.Common;
using RGIC.Repositories;
using System.Globalization;
using System.Security.Claims;
namespace RGICAPI.Controllers
{
    [Authorize]        
    [Route("api/[controller]")]
    [ApiController]
    public class BranchMasterController(IBranchRepo branch, IAuthLib authLib, IConfiguration configuration, ICommonRepo commonRepo) : BaseController
    {
        private readonly IBranchRepo _branch = branch;
        private readonly ICommonRepo _CommonRepo = commonRepo;
        private readonly IAuthLib _authLib = authLib;
        private readonly IConfiguration _configuration = configuration;


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DtoBranchvCreate branch)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //branch.CreatedBy = !string.IsNullOrEmpty(User?.Identity?.Name) ? new Guid(User.Identity.Name).ToString() : Guid.Empty.ToString();
            branch.CreatedBy = !string.IsNullOrEmpty(User?.Identity?.Name) ? new Guid(User.Identity.Name) : Guid.Empty;
            
            var result = await _branch.CreateBranch(branch);
            if (result.Status)
                return StatusCode(StatusCodes.Status201Created, result);
            return StatusCode(StatusCodes.Status409Conflict, result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] DtoBranchMaster branch)
        {
            var result = await _branch.UpdateBranch(branch);
            return Ok(result);
        }

        [HttpDelete("Delete/{branchId}")]
        public async Task<IActionResult> Delete(int branchId)
        {
            var result = await _branch.DeleteBranch(branchId);
            return Ok(result);
        }

        [HttpGet("Get/{branchId}")]
        public async Task<IActionResult> GetById(int branchId)
        {
            var result = await _branch.GetBranchById(branchId);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _branch.GetAllBranches();
            return Ok(result);
        }
    }

}

