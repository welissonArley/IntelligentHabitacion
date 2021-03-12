using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SetOfRules.Rule;
using IntelligentHabitacion.App.UseCases.User.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.App.UseCases.User.RegisterUser;
using XLabs.Ioc;

namespace IntelligentHabitacion.App
{
    public static class Bootstrapper
    {
        public static void Register(IDependencyContainer container)
        {
            container.Register(new UserPreferences());
            container.Register<IFriendRule, FriendRule>();
            container.Register<IHomeBrazilRule, HomeBrazilRule>();
            container.Register<IHomeOthersCountryRule, HomeOthersCountryRule>();
            container.Register<ILoginRule, LoginRule>();
            container.Register<IMyFoodsRule, MyFoodsRule>();
            container.Register<IUserRule, UserRule>();
            container.Register<ICleaningScheduleRule, CleaningScheduleRule>();

            container.Register<IEmailAlreadyBeenRegisteredUseCase, EmailAlreadyBeenRegisteredUseCase>();
            container.Register<IRegisterUserUseCase, RegisterUserUseCase>();
        }
    }
}
