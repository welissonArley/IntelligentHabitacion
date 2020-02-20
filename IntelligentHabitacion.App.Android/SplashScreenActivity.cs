using Android.App;
using Android.Content.PM;
using Android.OS;

namespace IntelligentHabitacion.App.Droid
{
    [Activity(Label = "Intelligent Habitacion", Theme = "@style/Theme.Splash", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, NoHistory = true)]
    public class SplashScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
        }

        public override void OnBackPressed() { /* Prevent the back button from canceling the startup process */ }
    }
}