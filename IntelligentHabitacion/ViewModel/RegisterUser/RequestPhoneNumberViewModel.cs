using IntelligentHabitacion.Model;
using IntelligentHabitacion.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel.RegisterUser
{
    public class RequestPhoneNumberViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        public ICommand NextCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public RequestPhoneNumberViewModel(IUserRule userRule)
        {
            _userRule = userRule;
            NextCommand = new Command(OnNext);
        }

        private void OnNext()
        {
            try
            {
                _userRule.ValidatePhoneNumber(Model.PhoneNumber1, Model.PhoneNumber2);

                Navigation.PushAsync<RequestEmailViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
