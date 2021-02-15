using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.ProcessFoodsNextToDueDate;
using IntelligentHabitacion.Api.Application.UseCases.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentHabitacion.Api.Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            return services
                .AddScoped<IntelligentHabitacionUseCase>()
                .AddScoped<IProcessFoodsNextToDueDate, ProcessFoodsNextToDueDateUseCasse>()
                .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }
    }
}
