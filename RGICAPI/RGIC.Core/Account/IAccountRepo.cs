using CRUDOperations;
using RGIC.Core.Account.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Account
{
    public interface IAccountRepo
    {
        Task<Response<string>> GenerateEncryptedToken(string userId, string email = "", string role = "");
        Task<Response> AccountCreation(DtoAccountCreation accountCreation);

        Task<Response<DtoUserInfoDetails>> Login(DtoLogin login);

        //Task<Response<DtoInvalidAttemptResponse>> InvalidLoginAttemptChecker(DtoInvalidAttemptChecker attemptChecker);

        

        //Task<Response<UserDetails>> GetUserBasicDetailsAsync(UserDetailsGetParam userDetailsGetParam);

       // Task<AuthResponse> SaveTokenAsync(UserDetailsGetParam userDetailsGetParam);

        
        //Task<Response<DtoForgotPassword>> ForgotPasswordAsync(DtoForgotPasswordRequest forgotPasswordRequest);

        //Task<Response<DtoForgotPassword>> ForgotPasswordVerifyAsync(DtoForgotPasswordRequest forgotPasswordRequest);
        //Task<Response> CheckValidEmailAsync(DtoForgotPasswordRequest? forgotPasswordRequest);

        //Task<AuthResponse> UserForgetPasswordValidatorAsync(DtoForgotPasswordValidatorAddParam forgotPasswordValidatorAddParam);

        

       // Task<Response> ChangePasswordUser(dtoChangePassword dtoChangePassword);








    }
}
