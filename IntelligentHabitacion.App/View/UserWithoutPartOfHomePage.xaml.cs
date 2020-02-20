using System.ComponentModel;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View
{
    [DesignTimeVisible(false)]
    public partial class UserWithoutPartOfHomePage : ContentPage
    {
        public UserWithoutPartOfHomePage()
        {
            InitializeComponent();

            var cardHeight = ((Application.Current.MainPage.Width / 2) - 35) * 1.27;

            GridCards.RowDefinitions.Clear();
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
        }
    }
}
