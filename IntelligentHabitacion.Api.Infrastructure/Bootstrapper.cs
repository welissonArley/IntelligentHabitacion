using IntelligentHabitacion.Api.Application.Services;
using IntelligentHabitacion.Api.Domain.Repository.Token;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Infrastructure.DataAccess;
using IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentHabitacion.Api.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IntelligentHabitacionContext>(options =>
                options.UseMySql(configuration.GetValue<string>("Settings:DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services
                .AddScoped<IUserWriteOnlyRepository, UserRepository>()
                .AddScoped<IUserReadOnlyRepository, UserRepository>()
                .AddScoped<IUserUpdateOnlyRepository, UserRepository>()
                .AddScoped<ITokenWriteOnlyRepository, TokenRepository>()
                .AddScoped<ITokenReadOnlyRepository, TokenRepository>();
        }
    }
}
