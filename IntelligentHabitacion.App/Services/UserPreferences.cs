using IntelligentHabitacion.App.Dtos;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IntelligentHabitacion.App.Services
{
    public class UserPreferences
    {
        private readonly string _keyEmail = "IHeml";
        private readonly string _keyPassword = "IHpawd";
        private readonly string _keyToken = "IHtkn";
        private readonly string _keyId = "IHidy";

        public string Name
        {
            get => Preferences.Get("NAME", null);
            private set => Preferences.Set("NAME", value);
        }
        public string ProfileColor
        {
            get => Preferences.Get("PROFILECOLOR", null);
            private set => Preferences.Set("PROFILECOLOR", value);
        }
        public bool IsPartOfOneHome
        {
            get => Preferences.Get("ISPARTOFONEHOME", false);
            private set => Preferences.Set("ISPARTOFONEHOME", value);
        }
        public bool IsAdministrator
        {
            get => Preferences.Get("ISADMINISTRATOR", false);
            private set
            {
                Preferences.Set("ISADMINISTRATOR", value);
                IsPartOfOneHome = true;
            }
        }
        #region asyncToRemove
        public string Token
        {
            get => Task.Run(async () => await SecureStorage.GetAsync(_keyToken)).Result;
            private set => SecureStorage.SetAsync(_keyToken, value);
        }
        public string Email
        {
            get => Task.Run(async () => await SecureStorage.GetAsync(_keyEmail)).Result;
            private set => SecureStorage.SetAsync(_keyEmail, value);
        }
        public string Password
        {
            get => Task.Run(async () => await SecureStorage.GetAsync(_keyPassword)).Result;
            private set => SecureStorage.SetAsync(_keyPassword, value);
        }
        public string Id
        {
            get => Task.Run(async () => await SecureStorage.GetAsync(_keyId)).Result;
            private set => SecureStorage.SetAsync(_keyId, value);
        }
        #endregion

        public double Width
        {
            get => Preferences.Get("WIDTH", 0.0);
            private set => Preferences.Set("WIDTH", value);
        }
        public bool HasOrder
        {
            get => Preferences.Get("HASORDER", false);
            private set => Preferences.Set("HASORDER", value);
        }

        #region functions
        public async Task SaveInitialUserInfos(UserPreferenceDto userPreference)
        {
            Name = userPreference.Name;
            ProfileColor = userPreference.ProfileColor;
            IsAdministrator = userPreference.IsAdministrator;
            IsPartOfOneHome = userPreference.IsPartOfOneHome;
            Width = userPreference.Width;
            await SecureStorage.SetAsync(_keyId, userPreference.Id);
            await SecureStorage.SetAsync(_keyEmail, userPreference.Email);
            await ChangePassword(userPreference.Password);
            await ChangeToken(userPreference.Password);
        }
        
        public void SaveUserInformations(UserPreferenceDto userPreference)
        {
            Name = userPreference.Name;
            Email = userPreference.Email;
            Password = userPreference.Password;
            ProfileColor = userPreference.ProfileColor;
            IsAdministrator = userPreference.IsAdministrator;
            IsPartOfOneHome = userPreference.IsPartOfOneHome;
            Token = userPreference.Token;
            Width = userPreference.Width;
            Id = userPreference.Id;
        }
        public void SaveUserInformations(string name, string email)
        {
            Name = name;
            Email = email;
        }
        public async Task ChangeToken(string token)
        {
            await SecureStorage.SetAsync(_keyToken, token);
        }
        public async Task ChangePassword(string password)
        {
            await SecureStorage.SetAsync(_keyPassword, password);
        }
        public void UserIsAdministrator(bool isAdmin)
        {
            IsAdministrator = isAdmin;
        }
        public void UserHasOrder(bool hasOrder)
        {
            HasOrder = hasOrder;
        }
        public void UserIsPartOfOneHome(bool isPartOfOneHome)
        {
            IsPartOfOneHome = isPartOfOneHome;
        }
        public bool AlreadySignedIn
        {
            get => !string.IsNullOrWhiteSpace(Task.Run(async() => await SecureStorage.GetAsync(_keyEmail)).Result)
                &&
                !string.IsNullOrWhiteSpace(Task.Run(async () => await SecureStorage.GetAsync(_keyPassword)).Result);
        }
        public bool HasAccessToken
        {
            get => !string.IsNullOrWhiteSpace(Task.Run(async () => await SecureStorage.GetAsync(_keyToken)).Result);
        }
        public void ClearAll()
        {
            Preferences.Clear();
            SecureStorage.RemoveAll();
        }
        public void Logout()
        {
            Preferences.Clear();
            SecureStorage.Remove(_keyToken);
            SecureStorage.Remove(_keyId);
        }
        #endregion
    }
}
