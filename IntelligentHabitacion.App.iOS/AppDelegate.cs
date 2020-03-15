using Foundation;
using IntelligentHabitacion.App.iOS.SQLite;
using IntelligentHabitacion.App.SQLite.Interface;
using System.Linq;
using System.Reflection;
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
            RoundedBoxView.Forms.Plugin.iOSUnified.RoundedBoxViewRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        private void ConfigureDI()
        {
            if (!Resolver.IsSet)
            {
                var container = new TinyContainer(new TinyIoCContainer());

                var listClassToUseDI = Assembly.Load("IntelligentHabitacion.App").GetExportedTypes().Where(tipo => !tipo.IsAbstract && !tipo.IsGenericType &&
                        tipo.GetInterfaces().Any(interfaces => !string.IsNullOrEmpty(interfaces.Name) && interfaces.Name.StartsWith("I") && (interfaces.Name.EndsWith("Rule") || interfaces.Name.EndsWith("Database"))));

                foreach (var classDI in listClassToUseDI)
                {
                    var interfaceToRegister = classDI.GetInterfaces().Single(i => i.Name.StartsWith("I") && i.Name.EndsWith("Rule"));
                    container.Register(interfaceToRegister, classDI);
                }

                var classHttpClientDI = Assembly.Load("IntelligentHabitacion.Communication").GetExportedTypes().First(tipo => !tipo.IsAbstract && !tipo.IsGenericType &&
                        tipo.GetInterfaces().Any(interfaces => !string.IsNullOrEmpty(interfaces.Name) && interfaces.Name.EndsWith("IIntelligentHabitacionHttpClient")));

                container.Register(classHttpClientDI.GetInterfaces().Single(i => i.Name.Equals("IIntelligentHabitacionHttpClient")), classHttpClientDI);

                container.Register<ISqliteConnection>(new SqliteDatabaseiOs());

                container.Register<IDependencyContainer>(container);

                Resolver.SetResolver(container.GetResolver());
            }
        }
    }
}
