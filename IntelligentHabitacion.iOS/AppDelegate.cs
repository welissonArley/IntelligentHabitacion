using Foundation;
using System.Linq;
using System.Reflection;
using TinyIoC;
using UIKit;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

namespace IntelligentHabitacion.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            
            ConfigureDI();

            Rg.Plugins.Popup.Popup.Init();
            RoundedBoxView.Forms.Plugin.iOSUnified.RoundedBoxViewRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void ConfigureDI()
        {
            if (!Resolver.IsSet)
            {
                var container = new TinyContainer(new TinyIoCContainer());

                var listClassToUseDI = Assembly.Load("IntelligentHabitacion").GetExportedTypes().Where(tipo => !tipo.IsAbstract && !tipo.IsGenericType &&
                        tipo.GetInterfaces().Any(interfaces => !string.IsNullOrEmpty(interfaces.Name) && interfaces.Name.StartsWith("I") && interfaces.Name.EndsWith("Rule")));

                foreach (var classDI in listClassToUseDI)
                {
                    var interfaceToRegister = classDI.GetInterfaces().Single(i => i.Name.StartsWith("I") && i.Name.EndsWith("Rule"));
                    container.Register(interfaceToRegister, classDI);
                }

                container.Register<IDependencyContainer>(container);

                Resolver.SetResolver(container.GetResolver());
            }
        }
    }
}
