using AuthLibrary;
using CRUDOperations;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using RGIC.Core.Account;
using RGIC.Core.Branch;
using RGIC.Core.Common;
using RGIC.Core.DataProtector;
using RGIC.Core.Product;
using RGIC.Infrastructure;
using RGIC.Repositories;
using RGICAPI.Services.Filters;

namespace RGICAPI.Services.Configs
{
    public static class DepandancyInjectionRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("connectionString");

            //// Register your services here
            services.AddSingleton<IAuthLib>(x => new DataAccess(x.GetService<IConfiguration>()!, connectionString!));
            services.AddSingleton<IDataProtectionRepo>(x => new DataProtectionRepo(x.GetService<IDataProtectionProvider>()!));
            services.AddScoped<ICrudOperationService>(x => new CrudOperationDataAccess(x.GetService<IConfiguration>()!, connectionString!));
            services.AddScoped<IAccountRepo, AccountRepository>();
            
            services.AddScoped<ICommonRepo, CommonRepo>();
            services.AddScoped<ActivityLogFilterAttribute>();
            services.AddScoped<ValidationFilter>();
            services.AddScoped<Utilities>();

            services.AddScoped<IBranchRepo, BranchRepository>();
            services.AddScoped<IProductRepo, ProductRepository>();
            return services;
        }
    }
}
