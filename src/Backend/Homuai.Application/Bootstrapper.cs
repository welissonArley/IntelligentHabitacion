using Homuai.Application.Services.Cryptography;
using Homuai.Application.Services.LoggedUser;
using Homuai.Application.Services.Token;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.User.RegisterUser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clearfield.Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped(options => new TokenController(configuration.GetValue<double>("Settings:Jwt:ExpiresMinutes"),
                    configuration.GetValue<string>("Settings:Jwt:SigningKey")))

                .AddScoped<HomuaiUseCase>()

                .AddScoped(options => new PasswordEncripter(configuration.GetValue<string>("Settings:KeyAdditionalCryptography")))

                .AddScoped<ILoggedUser, LoggedUser>()
                .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }
    }
}
