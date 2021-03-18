using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SetOfRules.Rule;
using IntelligentHabitacion.App.UseCases.Home.HomeInformations;
using IntelligentHabitacion.App.UseCases.Home.RegisterHome;
using IntelligentHabitacion.App.UseCases.Home.RegisterHome.Brazil;
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
            container.Register<IUserInformationsUseCase, UserInformationsUseCase>();
            container.Register<IUpdateUserInformationsUseCase, UpdateUserInformationsUseCase>();
            container.Register<IChangePasswordUseCase, ChangePasswordUseCase>();
            container.Register<ILoginUseCase, LoginUseCase>();
            container.Register<IRequestCodeResetPasswordUseCase, RequestCodeResetPasswordUseCase>();
            container.Register<IResetPasswordUseCase, ResetPasswordUseCase>();
            container.Register<IRequestCEPUseCase, RequestCEPUseCase>();
            container.Register<IRegisterHomeUseCase, RegisterHomeUseCase>();
            container.Register<IGetMyFoodsUseCase, GetMyFoodsUseCase>();
            container.Register<IRegisterMyFoodUseCase, RegisterMyFoodUseCase>();
            container.Register<IChangeQuantityOfOneProductUseCase, ChangeQuantityOfOneProductUseCase>();
            container.Register<IUpdateMyFoodUseCase, UpdateMyFoodUseCase>();
            container.Register<IDeleteMyFoodUseCase, DeleteMyFoodUseCase>();
            container.Register<IHomeInformationsUseCase, HomeInformationsUseCase>();
        }
    }
}
