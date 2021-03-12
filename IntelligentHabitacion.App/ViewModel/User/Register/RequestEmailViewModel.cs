using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.UseCases.User.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.App.View.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.User.Register
{
    public class RequestEmailViewModel : BaseViewModel
    {
        private readonly Lazy<IEmailAlreadyBeenRegisteredUseCase> useCase;
        private IEmailAlreadyBeenRegisteredUseCase _useCase => useCase.Value;

        public ICommand NextCommand { get; }
        public ICommand WhyINeedFillThisInformationCommand { get; }

        public RegisterUserModel Model { get; set; }

        public RequestEmailViewModel(Lazy<IEmailAlreadyBeenRegisteredUseCase> useCase)
        {
            this.useCase = useCase;

            NextCommand = new Command(async () => await OnNext());
            WhyINeedFillThisInformationCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new LgpdModal(ResourceText.DESCRIPTION_WHY_WE_NEED_YOUR_EMAIL));
            });
        }

        private async Task OnNext()
        {
            try
            {
                await ShowLoading();
                await _useCase.Execute(Model.Email);
                HideLoading();
                await Navigation.PushAsync<RequestNameViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
