using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IntelligentHabitacion.App.Services
{
    public class UserPreferences
    {
        public string Name
        {
            get => Preferences.Get("NAME", null);
            set => Preferences.Set("NAME", value);
        }
        public string ProfileColor
        {
            get => Preferences.Get("PROFILECOLOR", null);
            set => Preferences.Set("PROFILECOLOR", value);
        }
        public bool IsPartOfOneHome
        {
            get => Preferences.Get("ISPARTOFONEHOME", false);
            set => Preferences.Set("ISPARTOFONEHOME", value);
        }
        public bool IsAdministrator
        {
            get => Preferences.Get("ISADMINISTRATOR", false);
            set
            {
                Preferences.Set("ISADMINISTRATOR", value);
                IsPartOfOneHome = true;
            }
        }
        public string Token
        {
            get => Task.Run(async() => await SecureStorage.GetAsync("TOKEN")).Result;
            set => SecureStorage.SetAsync("TOKEN", value);
        }
        public double Width
        {
            get => Preferences.Get("WIDTH", 0.0);
            set => Preferences.Set("WIDTH", value);
        }
        public bool HasOrder
        {
            get => Preferences.Get("HASORDER", false);
            set => Preferences.Set("HASORDER", value);
        }

        public bool HasValue
        {
            get => Preferences.ContainsKey("NAME");
        }
        public void ClearAll()
        {
            Preferences.Clear();
            SecureStorage.RemoveAll();
        }
    }
}
