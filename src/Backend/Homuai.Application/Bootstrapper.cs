﻿using Homuai.Application.Services.Cryptography;
using Homuai.Application.Services.LoggedUser;
using Homuai.Application.Services.Token;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.CleaningSchedule.ProcessRemindersOfCleaningTasks;
using Homuai.Application.UseCases.Login.DoLogin;
using Homuai.Application.UseCases.Login.ForgotPassword;
using Homuai.Application.UseCases.MyFoods.ChangeQuantityOfOneProduct;
using Homuai.Application.UseCases.MyFoods.DeleteMyFood;
using Homuai.Application.UseCases.MyFoods.GetMyFoods;
using Homuai.Application.UseCases.MyFoods.ProcessFoodsNextToDueDate;
using Homuai.Application.UseCases.MyFoods.RegisterMyFood;
using Homuai.Application.UseCases.MyFoods.UpdateMyFood;
using Homuai.Application.UseCases.User.ChangePassword;
using Homuai.Application.UseCases.User.EmailAlreadyBeenRegistered;
using Homuai.Application.UseCases.User.RegisterUser;
using Homuai.Application.UseCases.User.UpdateUserInformations;
using Homuai.Application.UseCases.User.UserInformations;
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
                .AddScoped<IProcessRemindersOfCleaningTasksUseCase, ProcessRemindersOfCleaningTasksUseCase>()
                .AddScoped<IProcessFoodsNextToDueDateUseCase, ProcessFoodsNextToDueDateUseCase>()
                .AddScoped<IEmailAlreadyBeenRegisteredUseCase, EmailAlreadyBeenRegisteredUseCase>()
                .AddScoped<IUpdateUserInformationsUseCase, UpdateUserInformationsUseCase>()
                .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
                .AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>()
                .AddScoped<IUserInformationsUseCase, UserInformationsUseCase>()
                .AddScoped<IChangeQuantityOfOneProductUseCase, ChangeQuantityOfOneProductUseCase>()
                .AddScoped<IDeleteMyFoodUseCase, DeleteMyFoodUseCase>()
                .AddScoped<IGetMyFoodsUseCase, GetMyFoodsUseCase>()
                .AddScoped<IRegisterMyFoodUseCase, RegisterMyFoodUseCase>()
                .AddScoped<IUpdateMyFoodUseCase, UpdateMyFoodUseCase>()
                .AddScoped<ILoginUseCase, LoginUseCase>()
                .AddScoped<IRequestCodeResetPasswordUseCase, RequestCodeResetPasswordUseCase>()
                .AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
        }
    }
}
