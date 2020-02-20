using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.RegisterUser
{
    public class RequestEmergencyContact1ViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        public ICommand NextCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public RequestEmergencyContact1ViewModel(IUserRule userRule)
        {
            _userRule = userRule;
            NextCommand = new Command(OnNext);
        }

        private void OnNext()
        {
            try
            {
                _userRule.ValidateEmergencyContact(Model.EmergencyContact1.Name, Model.EmergencyContact1.PhoneNumber, Model.EmergencyContact1.FamilyRelationship);

                Navigation.PushAsync<RequestEmergencyContact2ViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
