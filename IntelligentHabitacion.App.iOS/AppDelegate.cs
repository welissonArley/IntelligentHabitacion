using Com.OneSignal;
using Foundation;
using IntelligentHabitacion.App.OneSignalConfig;
using TinyIoC;
using UIKit;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

namespace IntelligentHabitacion.App.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            
            ConfigureDI();

            Rg.Plugins.Popup.Popup.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            OneSignal.Current.StartInit(OneSignalManager.OneSignalKey).EndInit();
            Messier16.Forms.iOS.Controls.Messier16Controls.InitAll();

            LoadApplication(new App());

            Plugin.InputKit.Platforms.iOS.Config.Init();
            OneSignal.Current.RegisterForPushNotifications();

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        private void ConfigureDI()
        {
            if (!Resolver.IsSet)
            {
                var container = new TinyContainer(new TinyIoCContainer());

                Bootstrapper.Register(container);
                Communication.Bootstrapper.Register(container);

                container.Register<IDependencyContainer>(container);

                Resolver.SetResolver(container.GetResolver());
            }
        }
    }
}
