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

            var cardHeight = ((IntelligentHabitacionDevice.IntelligentHabitacionDevice.Width() / 2) - 35) * 1.27;

            GridCards.RowDefinitions.Clear();
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
        }

        protected override void OnAppearing()
        {
            HeaderGirlReading.FillInformations();
            base.OnAppearing();
        }
    }
}
