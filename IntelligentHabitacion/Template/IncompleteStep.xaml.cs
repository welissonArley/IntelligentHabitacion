using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.Template
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncompleteStep : ContentView
    {
        public IncompleteStep()
        {
            InitializeComponent();
        }

        public IncompleteStep(bool marginRight = false, bool marginLeft = false)
        {
            InitializeComponent();

            if (marginRight && marginLeft)
                component.Margin = new Thickness(15, 0, 15, 0);
            else if (marginLeft)
                component.Margin = new Thickness(15, 0, 0, 0);
            else if (marginRight)
                component.Margin = new Thickness(0, 0, 15, 0);
        }
    }
}