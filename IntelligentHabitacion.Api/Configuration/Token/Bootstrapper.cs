using IntelligentHabitacion.Api.Application.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentHabitacion.Api.Configuration.Token
{
    /// <summary>
    /// 
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddTokenController(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddScoped(ServiceProvider =>
            {
                return new TokenController(configuration.GetValue<double>("Settings:Jwt:ExpiresMinutes"),
                    configuration.GetValue<string>("Settings:Jwt:SigningKey"));
            });
        }
    }
}
