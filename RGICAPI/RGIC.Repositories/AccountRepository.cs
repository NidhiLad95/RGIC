using AuthLibrary;
using CRUDOperations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RGIC.Core.Account;
using RGIC.Core.Account.AccountDTOs;
using RGIC.Core.DataProtector;
using RGIC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Repositories
{
    public class AccountRepository(IAuthLib authLib, ICrudOperationService crudOperationService, IConfiguration configuration,
         IDataProtectionRepo dataProtectionService) : IAccountRepo
    {

        private readonly IAuthLib _authLib = authLib;
        private readonly ICrudOperationService _crudOperationService = crudOperationService;
        private readonly IConfiguration _configuration = configuration;
        private readonly IDataProtectionRepo _dataProtectionService = dataProtectionService;


        public async Task<Response> AccountCreation(DtoAccountCreation accountCreation)
        {
            Response res = new();
            DataAccess.CreatePasswordHash(accountCreation.Password!, out byte[] passwordHash, out byte[] passwordSalt);
            accountCreation.PasswordHash = passwordHash;
            accountCreation.PasswordSalt = passwordSalt;

            accountCreation.FirstName = Utilities.ToTitleCase(accountCreation.FirstName ?? "");

            res = await _authLib.SignUp<Response>("[UspSaveUsers]", accountCreation);
            DtoSavePassword objSavePassword = new DtoSavePassword();
            objSavePassword.UserId = res.Data;
            objSavePassword.NewPasswordHash = accountCreation.PasswordHash;
            objSavePassword.NewPasswordSalt = accountCreation.PasswordSalt;

            var result = await _authLib.SavePassword<Response>("[UspSavePassword]", objSavePassword);
            return res;
        }


        public async Task<Response<DtoUserInfoDetails>> Login(DtoLogin login)
        {


            var loginResponse = await _authLib.ValidateLoginCredentials<DtoUserInfoDetails>("[UspGetUserDetails]", new
            {

                login.Email,

            }, login.Password!).ConfigureAwait(false);

            

            return loginResponse;
        }
        public Task<Response<string>> GenerateEncryptedToken(string userId, string email = "", string role = "")
        {
            throw new NotImplementedException();
        }
    }
}
