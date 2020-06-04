using IntelligentHabitacion.App.SQLite.Interface;
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
            HeaderGirlReading.FillInformations();

            var database = XLabs.Ioc.Resolver.Resolve<ISqliteDatabase>();
            if (database.Get().HasOrder)
            {
                HeaderOrderHasArrived.IsVisible = true;
                HeaderGirlReading.IsVisible = false;
            }
            else
            {
                HeaderOrderHasArrived.IsVisible = false;
                HeaderGirlReading.IsVisible = true;
            }

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
            var database = XLabs.Ioc.Resolver.Resolve<ISqliteDatabase>();
            database.GotTheOrder();
            HeaderOrderHasArrived.IsVisible = false;
            HeaderGirlReading.IsVisible = true;
        }
    }
}
