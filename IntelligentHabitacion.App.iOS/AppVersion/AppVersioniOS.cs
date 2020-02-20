using Foundation;
using IntelligentHabitacion.App.AppVersion;
using IntelligentHabitacion.App.iOS.AppVersion;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersioniOS))]
namespace IntelligentHabitacion.App.iOS.AppVersion
{
    public class AppVersioniOS : IVersaoApp
    {
        public string GetVersionNumber()
        {
            var info = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"];

            return $"{info.Description}.0";
        }
    }
}