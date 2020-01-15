using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.Template.CardIlustrationOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardAboutApp : ContentView
    {
        public CardAboutApp()
        {
            InitializeComponent();

            var cardHeight = ((Application.Current.MainPage.Width / 2) - 35) * 0.54;

            GridCard.RowDefinitions.Clear();
            GridCard.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
        }
    }
}