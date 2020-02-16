using Foundation;
using IntelligentHabitacion.AppVersion;
using IntelligentHabitacion.iOS.AppVersion;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersioniOS))]
namespace IntelligentHabitacion.iOS.AppVersion
{
    public class AppVersioniOS : IVersaoApp
    {
        public string GetVersionNumber()
        {
            var info = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"];

            return $"{info.Description}.0.0";
        }
    }
}