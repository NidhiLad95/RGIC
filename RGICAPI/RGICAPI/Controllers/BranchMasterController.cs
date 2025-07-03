using AuthLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RGIC.Core.Branch;
using RGIC.Core.Common;
using RGIC.Repositories;
using System.Globalization;
namespace RGICAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchMasterController(IBranchRepo branch, IAuthLib authLib, IConfiguration configuration, ICommonRepo commonRepo) : BaseController
    {
        private readonly IBranchRepo _branch = branch;
        private readonly ICommonRepo _CommonRepo = commonRepo;
        private readonly IAuthLib _authLib = authLib;
        private readonly IConfiguration _configuration = configuration;

    }
}
