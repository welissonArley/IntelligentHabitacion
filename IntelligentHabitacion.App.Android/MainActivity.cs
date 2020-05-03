using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using IntelligentHabitacion.App.Droid.SQLite;
using IntelligentHabitacion.App.SQLite.Interface;
using System.Linq;
using System.Reflection;
using TinyIoC;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

namespace IntelligentHabitacion.App.Droid
{
    [Activity(Label = "Intelligent Habitacion", Icon = "@mipmap/ic_launcher", RoundIcon = "@mipmap/ic_launcher_round", Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            ConfigureDI();

            RoundedBoxView.Forms.Plugin.Droid.RoundedBoxViewRenderer.Init();
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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
                    var interfaceToRegister = classDI.GetInterfaces().Single(i => i.Name.StartsWith("I") && (i.Name.EndsWith("Rule") || i.Name.EndsWith("Database")));
                    container.Register(interfaceToRegister, classDI);
                }

                var classHttpClientDI = Assembly.Load("IntelligentHabitacion.Communication").GetExportedTypes().First(tipo => !tipo.IsAbstract && !tipo.IsGenericType &&
                        tipo.GetInterfaces().Any(interfaces => !string.IsNullOrEmpty(interfaces.Name) && interfaces.Name.EndsWith("IIntelligentHabitacionHttpClient")));

                container.Register(classHttpClientDI.GetInterfaces().Single(i => i.Name.Equals("IIntelligentHabitacionHttpClient")), classHttpClientDI);

                container.Register<ISqliteConnection>(new SqliteDatabaseAndroid());

                container.Register<IDependencyContainer>(container);

                Resolver.SetResolver(container.GetResolver());
            }
        }

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }
    }
}