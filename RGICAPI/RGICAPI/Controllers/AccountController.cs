using AuthLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RGIC.Core.Account;
using RGIC.Core.Account.AccountDTOs;
using RGIC.Core.Common;
using RGIC.Repositories;
using System.Globalization;

namespace RGICAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController (IAccountRepo account, IAuthLib authLib, IConfiguration configuration,ICommonRepo commonRepo) : BaseController
    {
        private readonly IAccountRepo _account = account;
        private readonly ICommonRepo _CommonRepo = commonRepo;
        private readonly IAuthLib _authLib = authLib;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost, Route("AccountCreate")]
        public async Task<IActionResult> AccountCreation([FromBody] DtoAccountCreation? accountCreation)
        {
            accountCreation ??= new();
            if (!string.IsNullOrEmpty(User?.Identity?.Name))
            {
                accountCreation.CreatedBy = new Guid(User.Identity.Name);
            }
            else
            {
                accountCreation.CreatedBy = Guid.Empty;
            }
            accountCreation.CreatedBy = accountCreation.CreatedBy;
            var response = await _account.AccountCreation(accountCreation).ConfigureAwait(false);

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

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] DtoLogin dtoLogin)
        {
            var response = await _account.Login(dtoLogin).ConfigureAwait(false);
            var userInfoRes = response.Data!;

            if (!response.Status)
            {
                response.Message = response.Message!.Trim().ToLower(CultureInfo.CurrentCulture);
            }

            if (response.Status)
            {

                {

                    var token = await _authLib.GenerateJWTToken(userInfoRes.UserId.ToString()!, userInfoRes.Email!, "admin", expiration: DateTime.Now.AddDays(1)).ConfigureAwait(false);

                    await _authLib.SaveToken("[UspTokenSave]", new
                    {
                        userInfoRes.UserId,
                        token,
                    }).ConfigureAwait(false);


                    if (!response.Status)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Something went wrong, Please contact system administrator");
                    }
                    return Ok(new
                    {
                        userInfoRes.UserId,
                        userInfoRes.Email,
                        userInfoRes.FirstName,                        
                        userInfoRes.LastName,
                        Message = "successfully logged into the system",
                        LoginStatus = response.Message,
                        token,                        
                        response.Status,              

                    });
                }
            }            
            else
            {
                response.Message = "Something went wrong, Please contact system administrator";
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }


        }


    }
}
