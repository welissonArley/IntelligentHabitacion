using IntelligentHabitacion.Api.Application.Interfaces.UseCases;
using IntelligentHabitacion.Api.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentHabitacion.Api.Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            return services
                .AddScoped<IProcessFoodsNextToDueDate, ProcessFoodsNextToDueDateUseCasse>();
        }
    }
}
