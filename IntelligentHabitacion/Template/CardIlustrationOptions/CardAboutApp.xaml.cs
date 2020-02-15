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
        }
    }
}