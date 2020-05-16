using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.App.View;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel.RegisterUser
{
    public class RequestPasswordViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        private readonly ISqliteDatabase _database;

        public ICommand OnConcludeCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public RequestPasswordViewModel(IUserRule userRule, ISqliteDatabase database)
        {
            _userRule = userRule;
            _database = database;
            OnConcludeCommand = new Command(async () => await OnConclude());
        }

        private async Task OnConclude()
        {
            try
            {
                await ShowLoading();

                _userRule.ValidatePassword(Model.Password, Model.PasswordConfirmation);

                var response = await _userRule.Create(Model);

                _database.Save(new SQLite.Model.UserSqlite
                {
                    Name = Model.Name,
                    IsAdministrator = false,
                    IsPartOfOneHome = false,
                    Width = Application.Current.MainPage.Width,
                    Token = response.Token,
                    ProfileColor = response.Response.ToString()
                });

                Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomeViewModel, UserWithoutPartOfHomePage>());

                HideLoading();

                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
