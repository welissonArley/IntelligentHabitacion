using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.AddFriends;
using IntelligentHabitacion.Api.Application.UseCases.ChangeDateFriendJoinHome;
using IntelligentHabitacion.Api.Application.UseCases.ChangePassword;
using IntelligentHabitacion.Api.Application.UseCases.ChangeQuantityOfOneProduct;
using IntelligentHabitacion.Api.Application.UseCases.DeleteMyFood;
using IntelligentHabitacion.Api.Application.UseCases.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.Api.Application.UseCases.ForgotPassword;
using IntelligentHabitacion.Api.Application.UseCases.GetMyFoods;
using IntelligentHabitacion.Api.Application.UseCases.GetMyFriends;
using IntelligentHabitacion.Api.Application.UseCases.HomeInformations;
using IntelligentHabitacion.Api.Application.UseCases.Login;
using IntelligentHabitacion.Api.Application.UseCases.ProcessFoodsNextToDueDate;
using IntelligentHabitacion.Api.Application.UseCases.RegisterHome;
using IntelligentHabitacion.Api.Application.UseCases.RegisterMyFood;
using IntelligentHabitacion.Api.Application.UseCases.RegisterUser;
using IntelligentHabitacion.Api.Application.UseCases.ChangeAdministrator;
using IntelligentHabitacion.Api.Application.UseCases.UpdateHomeInformations;
using IntelligentHabitacion.Api.Application.UseCases.UpdateMyFood;
using IntelligentHabitacion.Api.Application.UseCases.UpdateUserInformations;
using IntelligentHabitacion.Api.Application.UseCases.UserInformations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IntelligentHabitacion.Api.Application.UseCases.RemoveFriend;
using IntelligentHabitacion.Api.Application.UseCases.NotifyOrderReceived;

namespace IntelligentHabitacion.Api.Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IntelligentHabitacionUseCase>()
                .AddScoped(options => new PasswordEncripter(configuration.GetValue<string>("Settings:KeyAdditionalCryptography")))
                .AddScoped<ILoggedUser, LoggedUser>()
                .AddScoped<IProcessFoodsNextToDueDate, ProcessFoodsNextToDueDateUseCasse>()
                .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
                .AddScoped<IEmailAlreadyBeenRegisteredUseCase, EmailAlreadyBeenRegisteredUseCase>()
                .AddScoped<IUserInformationsUseCase, UserInformationsUseCase>()
                .AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>()
                .AddScoped<IUpdateUserInformationsUseCase, UpdateUserInformationsUseCase>()
                .AddScoped<IRequestCodeResetPasswordUseCase, RequestCodeResetPasswordUseCase>()
                .AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>()
                .AddScoped<ILoginUseCase, LoginUseCase>()
                .AddScoped<IRegisterHomeUseCase, RegisterHomeUseCase>()
                .AddScoped<IHomeInformationsUseCase, HomeInformationsUseCase>()
                .AddScoped<IUpdateHomeInformationsUseCase, UpdateHomeInformationsUseCase>()
                .AddScoped<IGetMyFoodsUseCase, GetMyFoodsUseCase>()
                .AddScoped<IRegisterMyFoodUseCase, RegisterMyFoodUseCase>()
                .AddScoped<IChangeQuantityOfOneProductUseCase, ChangeQuantityOfOneProductUseCase>()
                .AddScoped<IDeleteMyFoodUseCase, DeleteMyFoodUseCase>()
                .AddScoped<IUpdateMyFoodUseCase, UpdateMyFoodUseCase>()
                .AddScoped<IGetMyFriendsUseCase, GetMyFriendsUseCase>()
                .AddScoped<IAddFriendUseCase, AddFriendUseCase>()
                .AddScoped<IChangeDateFriendJoinHomeUseCase, ChangeDateFriendJoinHomeUseCase>()
                .AddScoped<IRequestCodeChangeAdministratorUseCase, RequestCodeChangeAdministratorUseCase>()
                .AddScoped<IChangeAdministratorUseCase, ChangeAdministratorUseCase>()
                .AddScoped<IRequestCodeToRemoveFriendUseCase, RequestCodeToRemoveFriendUseCase>()
                .AddScoped<IRemoveFriendUseCase, RemoveFriendUseCase>()
                .AddScoped<INotifyOrderReceivedUseCase, NotifyOrderReceivedUseCase>();
        }
    }
}
