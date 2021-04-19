using Foundation;
using IntelligentHabitacion.App.iOS.AppVersion;
using IntelligentHabitacion.App.Services.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersioniOS))]
namespace IntelligentHabitacion.App.iOS.AppVersion
{
    public class AppVersioniOS : IAppVersion
    {
        public string GetVersionNumber()
        {
            var info = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"];

            return $"{info.Description}.0";
        }
    }
}