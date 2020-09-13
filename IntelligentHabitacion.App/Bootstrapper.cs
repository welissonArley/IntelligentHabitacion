using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SetOfRules.Rule;
using XLabs.Ioc;

namespace IntelligentHabitacion.App
{
    public static class Bootstrapper
    {
        public static void Register(IDependencyContainer container)
        {
            container.Register(new UserPreferences());
            container.Register<IFriendRule, FriendRule>();
            container.Register<IHomeRule, HomeRule>();
            container.Register<ILoginRule, LoginRule>();
            container.Register<IMyFoodsRule, MyFoodsRule>();
            container.Register<IUserRule, UserRule>();
        }
    }
}
