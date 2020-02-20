using IntelligentHabitacion.App.AppVersion;
using IntelligentHabitacion.App.Droid.AppVersion;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersionAndroid))]
namespace IntelligentHabitacion.App.Droid.AppVersion
{
    public class AppVersionAndroid : IVersaoApp
    {
        public string GetVersionNumber()
        {
            var context = Android.App.Application.Context;
            var manager = context.PackageManager;
            var info = manager.GetPackageInfo(context.PackageName, 0);

            return $"{info.VersionName}.0";
        }
    }
}