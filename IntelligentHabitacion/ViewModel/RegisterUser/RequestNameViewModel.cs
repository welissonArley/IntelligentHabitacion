using IntelligentHabitacion.Model;
using IntelligentHabitacion.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel.RegisterUser
{
    public class RequestNameViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        public ICommand NextCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public RequestNameViewModel(IUserRule userRule)
        {
            _userRule = userRule;
            NextCommand = new Command(OnNext);
        }

        private void OnNext()
        {
            try
            {
                _userRule.ValidateName(Model.Name);

                Navigation.PushAsync<RequestPhoneNumberViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
