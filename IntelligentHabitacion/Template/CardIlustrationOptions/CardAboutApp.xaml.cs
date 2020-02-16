using IntelligentHabitacion.AppVersion;
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

            LabelVersionNumber.Text = DependencyService.Get<IVersaoApp>().GetVersionNumber();

            GridCard.RowDefinitions.Clear();
            GridCard.RowDefinitions.Add(new RowDefinition { Height = Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet ? 100 : 50 });
        }
    }
}