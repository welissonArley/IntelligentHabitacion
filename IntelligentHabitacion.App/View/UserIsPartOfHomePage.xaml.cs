using IntelligentHabitacion.App.Services;
using System.ComponentModel;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View
{
    [DesignTimeVisible(false)]
    public partial class UserIsPartOfHomePage : ContentPage
    {
        public UserIsPartOfHomePage()
        {
            InitializeComponent();

            SetGridDefinitions();

            HeaderOrderHasArrived.ButtonClickedCommand = new Command(UserGotOrder);
        }

        protected override void OnAppearing()
        {
            RefreshHeader();
            base.OnAppearing();
        }

        private void SetGridDefinitions()
        {
            var cardHeight = ((IntelligentHabitacionDevice.IntelligentHabitacionDevice.Width() / 2) - 35) * 1.27;

            GridCards.RowDefinitions.Clear();
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
        }

        private void UserGotOrder()
        {
            var userPreferences = XLabs.Ioc.Resolver.Resolve<UserPreferences>();
            userPreferences.HasOrder = false;
            HeaderOrderHasArrived.IsVisible = false;
            HeaderGirlReading.IsVisible = true;
        }

        public void RefreshHeader()
        {
            HeaderGirlReading.FillInformations();

            var userPreferences = XLabs.Ioc.Resolver.Resolve<UserPreferences>();
            if (userPreferences.HasOrder)
            {
                HeaderOrderHasArrived.IsVisible = true;
                HeaderGirlReading.IsVisible = false;
            }
            else
            {
                HeaderOrderHasArrived.IsVisible = false;
                HeaderGirlReading.IsVisible = true;
            }
        }
    }
}
