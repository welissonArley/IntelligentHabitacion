using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.UseCases.Home.RegisterHome;
using IntelligentHabitacion.App.UseCases.Home.RegisterHome.Brazil;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.ViewModel.Login;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Home.Register
{
    public class RegisterHomeViewModel : BaseViewModel
    {
        private readonly Lazy<IRequestCEPUseCase> cepUseCase;
        private readonly Lazy<IRegisterHomeUseCase> useCase;
        private IRegisterHomeUseCase _useCase => useCase.Value;
        private IRequestCEPUseCase _cepUseCase => cepUseCase.Value;

        public HomeModel Model { get; set; }
        private string _currentZipCode { get; set; }

        public EventHandler<FocusEventArgs> ZipCodeChangedUnfocused { get; }
        public ICommand OnConcludeCommand { get; }
        public ICommand WhyINeedFillThisInformationCommand { get; }

        public RegisterHomeViewModel(Lazy<IRegisterHomeUseCase> useCase, Lazy<IRequestCEPUseCase> cepUseCase)
        {
            this.useCase = useCase;
            this.cepUseCase = cepUseCase;

            OnConcludeCommand = new Command(async () => await OnConclude());
            WhyINeedFillThisInformationCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new LgpdModal(ResourceText.DESCRIPTION_WHY_WE_NEED_YOUR_ADDRESS));
            });

            ZipCodeChangedUnfocused += async (sender, e) =>
            {
                await VerifyZipCode();
            };

            _currentZipCode = "";
        }

        private async Task OnConclude()
        {
            try
            {
                SendingData();

                await _useCase.Execute(Model);

                await Sucess();

                Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserIsPartOfHomeViewModel, UserIsPartOfHomePage>());

                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task VerifyZipCode()
        {
            try
            {
                if (Model.City.Country.Id != Useful.CountryEnum.BRAZIL || _currentZipCode.Equals(Model.ZipCode))
                    return;

                await ShowLoading();
                
                var result = await _cepUseCase.Execute(Model.ZipCode);

                _currentZipCode = Model.ZipCode;
                Model.Neighborhood = result.Neighborhood;
                Model.Address = result.Address;
                Model.City.Name = result.City.Name;
                Model.City.StateProvinceName = result.City.StateProvinceName;

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
