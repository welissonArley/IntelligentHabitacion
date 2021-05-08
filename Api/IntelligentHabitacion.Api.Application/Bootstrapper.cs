using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.Calendar;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.CreateFirstSchedule;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.DetailsAllRate;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.EditTaskAssign;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetTasks;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.HistoryOfTheDay;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.ProcessRemindersOfCleaningTasks;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.RateTask;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.RegisterRoomCleaned;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.Reminder;
using IntelligentHabitacion.Api.Application.UseCases.Friends.AddFriends;
using IntelligentHabitacion.Api.Application.UseCases.Friends.ChangeAdministrator;
using IntelligentHabitacion.Api.Application.UseCases.Friends.ChangeDateFriendJoinHome;
using IntelligentHabitacion.Api.Application.UseCases.Friends.GetMyFriends;
using IntelligentHabitacion.Api.Application.UseCases.Friends.NotifyOrderReceived;
using IntelligentHabitacion.Api.Application.UseCases.Friends.RemoveFriend;
using IntelligentHabitacion.Api.Application.UseCases.Home.HomeInformations;
using IntelligentHabitacion.Api.Application.UseCases.Home.RegisterHome;
using IntelligentHabitacion.Api.Application.UseCases.Home.UpdateHomeInformations;
using IntelligentHabitacion.Api.Application.UseCases.Login.DoLogin;
using IntelligentHabitacion.Api.Application.UseCases.Login.ForgotPassword;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.ChangeQuantityOfOneProduct;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.DeleteMyFood;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.GetMyFoods;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.ProcessFoodsNextToDueDate;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.RegisterMyFood;
using IntelligentHabitacion.Api.Application.UseCases.MyFoods.UpdateMyFood;
using IntelligentHabitacion.Api.Application.UseCases.User.ChangePassword;
using IntelligentHabitacion.Api.Application.UseCases.User.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.Api.Application.UseCases.User.RegisterUser;
using IntelligentHabitacion.Api.Application.UseCases.User.UpdateUserInformations;
using IntelligentHabitacion.Api.Application.UseCases.User.UserInformations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .AddScoped<IProcessFoodsNextToDueDateUseCase, ProcessFoodsNextToDueDateUseCase>()
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
                .AddScoped<INotifyOrderReceivedUseCase, NotifyOrderReceivedUseCase>()
                .AddScoped<IProcessRemindersOfCleaningTasksUseCase, ProcessRemindersOfCleaningTasksUseCase>()
                .AddScoped<IGetTasksUseCase, GetTasksUseCase>()
                .AddScoped<ICreateFirstScheduleUseCase, CreateFirstScheduleUseCase>()
                .AddScoped<IRegisterRoomCleanedUseCase, RegisterRoomCleanedUseCase>()
                .AddScoped<IReminderUseCase, ReminderUseCase>()
                .AddScoped<ICalendarUseCase, CalendarUseCase>()
                .AddScoped<IHistoryOfTheDayUseCase, HistoryOfTheDayUseCase>()
                .AddScoped<IEditTaskAssignUseCase, EditTaskAssignUseCase>()
                .AddScoped<IRateTaskUseCase, RateTaskUseCase>()
                .AddScoped<IDetailsAllRateUseCase, DetailsAllRateUseCase>();
        }
    }
}
