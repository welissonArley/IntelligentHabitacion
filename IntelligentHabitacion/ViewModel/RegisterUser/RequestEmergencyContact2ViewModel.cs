using IntelligentHabitacion.Model;
using IntelligentHabitacion.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel.RegisterUser
{
    public class RequestEmergencyContact2ViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        public ICommand NextCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public RequestEmergencyContact2ViewModel(IUserRule userRule)
        {
            _userRule = userRule;
            NextCommand = new Command(OnNext);
        }

        private void OnNext()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Model.EmergencyContact2.Name))
                    _userRule.ValidateEmergencyContact(Model.EmergencyContact2.Name, Model.EmergencyContact2.PhoneNumber, Model.EmergencyContact2.FamilyRelationship);

                Navigation.PushAsync<RequestPasswordViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
