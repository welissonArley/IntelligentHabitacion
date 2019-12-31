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
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void ConfigureDI()
        {
            var container = TinyIoCContainer.Current;

            var listClassToUseDI = Assembly.Load("IntelligentHabitacion.SetOfRules").GetExportedTypes().Where(tipo => !tipo.IsAbstract && !tipo.IsGenericType &&
                    tipo.GetInterfaces().Any(interfaces => !string.IsNullOrEmpty(interfaces.Name) && interfaces.Name.StartsWith("I") && interfaces.Name.EndsWith("Rule")));

            foreach (var classDI in listClassToUseDI)
            {
                var interfaceToRegister = classDI.GetInterfaces().Single(i => i.Name.StartsWith("I") && i.Name.EndsWith("Rule"));
                container.Register(interfaceToRegister, classDI);
            }

            Resolver.SetResolver(new TinyResolver(container));
        }
    }
}
