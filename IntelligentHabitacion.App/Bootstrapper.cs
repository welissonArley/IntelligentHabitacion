using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.Calendar;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.CreateFirstSchedule;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.GetTasks;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.HistoryOfTheDay;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.RegisterRoomCleaned;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.Reminder;
using IntelligentHabitacion.App.UseCases.Friends.ChangeDateFriendJoinHome;
using IntelligentHabitacion.App.UseCases.Friends.GetMyFriends;
using IntelligentHabitacion.App.UseCases.Friends.NotifyOrderReceived;
using IntelligentHabitacion.App.UseCases.Friends.RemoveFriend;
using IntelligentHabitacion.App.UseCases.Home.HomeInformations;
using IntelligentHabitacion.App.UseCases.Home.RegisterHome;
using IntelligentHabitacion.App.UseCases.Home.RegisterHome.Brazil;
using IntelligentHabitacion.App.UseCases.Home.UpdateHomeInformations;
using IntelligentHabitacion.App.UseCases.Login.DoLogin;
using IntelligentHabitacion.App.UseCases.Login.ForgotPassword;
using IntelligentHabitacion.App.UseCases.MyFoods.ChangeQuantityOfOneProduct;
using IntelligentHabitacion.App.UseCases.MyFoods.DeleteMyFood;
using IntelligentHabitacion.App.UseCases.MyFoods.GetMyFoods;
using IntelligentHabitacion.App.UseCases.MyFoods.RegisterMyFood;
using IntelligentHabitacion.App.UseCases.MyFoods.UpdateMyFood;
using IntelligentHabitacion.App.UseCases.User.ChangePassword;
using IntelligentHabitacion.App.UseCases.User.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.App.UseCases.User.RegisterUser;
using IntelligentHabitacion.App.UseCases.User.UpdateUserInformations;
using IntelligentHabitacion.App.UseCases.User.UserInformations;
using XLabs.Ioc;

namespace IntelligentHabitacion.App
{
    public static class Bootstrapper
    {
        public static IDependencyContainer AddDependeces(this IDependencyContainer container)
        {
            return container.Register(new UserPreferences())
                .Register<IEmailAlreadyBeenRegisteredUseCase, EmailAlreadyBeenRegisteredUseCase>()
                .Register<IRegisterUserUseCase, RegisterUserUseCase>()
                .Register<IUserInformationsUseCase, UserInformationsUseCase>()
                .Register<IUpdateUserInformationsUseCase, UpdateUserInformationsUseCase>()
                .Register<IChangePasswordUseCase, ChangePasswordUseCase>()
                .Register<ILoginUseCase, LoginUseCase>()
                .Register<IRequestCodeResetPasswordUseCase, RequestCodeResetPasswordUseCase>()
                .Register<IResetPasswordUseCase, ResetPasswordUseCase>()
                .Register<IRequestCEPUseCase, RequestCEPUseCase>()
                .Register<IRegisterHomeUseCase, RegisterHomeUseCase>()
                .Register<IGetMyFoodsUseCase, GetMyFoodsUseCase>()
                .Register<IRegisterMyFoodUseCase, RegisterMyFoodUseCase>()
                .Register<IChangeQuantityOfOneProductUseCase, ChangeQuantityOfOneProductUseCase>()
                .Register<IUpdateMyFoodUseCase, UpdateMyFoodUseCase>()
                .Register<IDeleteMyFoodUseCase, DeleteMyFoodUseCase>()
                .Register<IHomeInformationsUseCase, HomeInformationsUseCase>()
                .Register<IUpdateHomeInformationsUseCase, UpdateHomeInformationsUseCase>()
                .Register<IGetMyFriendsUseCase, GetMyFriendsUseCase>()
                .Register<INotifyOrderReceivedUseCase, NotifyOrderReceivedUseCase>()
                .Register<IChangeDateFriendJoinHomeUseCase, ChangeDateFriendJoinHomeUseCase>()
                .Register<IRemoveFriendUseCase, RemoveFriendUseCase>()
                .Register<IRequestCodeToRemoveFriendUseCase, RequestCodeToRemoveFriendUseCase>()
                .Register<IGetTasksUseCase, GetTasksUseCase>()
                .Register<ICreateFirstScheduleUseCase, CreateFirstScheduleUseCase>()
                .Register<IRegisterRoomCleanedUseCase, RegisterRoomCleanedUseCase>()
                .Register<IReminderUseCase, ReminderUseCase>()
                .Register<ICalendarUseCase, CalendarUseCase>()
                .Register<IHistoryOfTheDayUseCase, HistoryOfTheDayUseCase>();
        }
    }
}
