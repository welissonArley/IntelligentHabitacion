using IntelligentHabitacion.App.Services.Interface;
using IntelligentHabitacion.App.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.CardIlustrationOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardAboutApp : ContentView
    {
        public CardAboutApp()
        {
            InitializeComponent();

            LabelVersionNumber.Text = DependencyService.Get<IAppVersion>().GetVersionNumber();

            GridCard.RowDefinitions.Clear();
            GridCard.RowDefinitions.Add(new RowDefinition { Height = Device.Idiom == TargetIdiom.Tablet ? 100 : 50 });
        }

        private void Card_OnTapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AboutThisAppPage());
        }
    }
}