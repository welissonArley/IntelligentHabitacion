using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingTemplate : ContentView
    {
        public string Text { set { LabelText.Text = value; } get { return LabelText.Text; } }

        public LoadingTemplate()
        {
            InitializeComponent();
        }
    }
}