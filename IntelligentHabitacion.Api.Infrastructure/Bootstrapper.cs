using IntelligentHabitacion.Api.Application.Services;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Infrastructure.DataAccess;
using IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentHabitacion.Api.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<IntelligentHabitacionContext>(options =>
                options.UseMySql("Server=localhost;Database=intelligenthabitacion;Uid=root;Pwd=@Ioasys;"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services
                .AddScoped<IUserWriteOnlyRepository, UserRepository>();
        }
    }
}
