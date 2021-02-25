using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using IntelligentHabitacion.Api.Domain.Repository.Home;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using IntelligentHabitacion.Api.Domain.Repository.Token;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Api.Infrastructure.DataAccess;
using IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories;
using IntelligentHabitacion.Api.Infrastructure.Services;
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
            services.AddScoped<ISendEmail, SendEmail>(options =>
            {
                return new SendEmail(new Domain.ValueObjects.EmailConfig
                {
                    ApiKey = configuration.GetValue<string>("Settings:SendEmailSettings:ApiKey"),
                    Name = "Intelligent Habitacion Team",
                    Email = configuration.GetValue<string>("Settings:SendEmailSettings:FromEmail")
                });
            });
            services.AddScoped<IPushNotificationService, OneSignalService>(options =>
            {
                return new OneSignalService(new Domain.ValueObjects.OneSignalConfig
                {
                    AppId = configuration.GetValue<string>("Settings:OneSignal:AppId"),
                    Url = configuration.GetValue<string>("Settings:OneSignal:Url"),
                    Key = configuration.GetValue<string>("Settings:OneSignal:Key")
                });
            });

            return services
                .AddScoped<IUserWriteOnlyRepository, UserRepository>()
                .AddScoped<IUserReadOnlyRepository, UserRepository>()
                .AddScoped<IUserUpdateOnlyRepository, UserRepository>()
                .AddScoped<ITokenWriteOnlyRepository, TokenRepository>()
                .AddScoped<ITokenReadOnlyRepository, TokenRepository>()
                .AddScoped<ICodeReadOnlyRepository, CodeRepository>()
                .AddScoped<ICodeWriteOnlyRepository, CodeRepository>()
                .AddScoped<IHomeWriteOnlyRepository, HomeRepository>()
                .AddScoped<IHomeUpdateOnlyRepository, HomeRepository>()
                .AddScoped<IMyFoodsReadOnlyRepository, MyFoodsRepository>()
                .AddScoped<IMyFoodsUpdateOnlyRepository, MyFoodsRepository>()
                .AddScoped<IMyFoodsWriteOnlyRepository, MyFoodsRepository>();
        }
    }
}
