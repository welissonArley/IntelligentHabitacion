using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.Api.Application.UseCases.ProcessFoodsNextToDueDate;
using IntelligentHabitacion.Api.Application.UseCases.RegisterUser;
using IntelligentHabitacion.Api.Application.UseCases.UpdateUserInformations;
using IntelligentHabitacion.Api.Application.UseCases.UserInformations;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentHabitacion.Api.Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            return services
                .AddScoped<IntelligentHabitacionUseCase>()
                .AddScoped<ILoggedUser, LoggedUser>()
                .AddScoped<IProcessFoodsNextToDueDate, ProcessFoodsNextToDueDateUseCasse>()
                .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
                .AddScoped<IEmailAlreadyBeenRegisteredUseCase, EmailAlreadyBeenRegisteredUseCase>()
                .AddScoped<IUserInformationsUseCase, UserInformationsUseCase>()
                .AddScoped<IUpdateUserInformationsUseCase, UpdateUserInformationsUseCase>();
        }
    }
}
