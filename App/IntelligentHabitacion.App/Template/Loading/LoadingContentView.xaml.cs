using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingContentView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LoadingContentView()
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
        }
    }
}