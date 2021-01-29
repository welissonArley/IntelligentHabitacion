using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Button
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SegmentedControl : ContentView
    {
        public string TitleOption1
        {
            set
            {
                LabelOption1.Text = value;
            }
            get { return LabelOption1.Text; }
        }
        public string TitleOption2
        {
            set
            {
                LabelOption2.Text = value;
            }
            get { return LabelOption2.Text; }
        }

        public SegmentedControl()
        {
            InitializeComponent();
        }
    }
}