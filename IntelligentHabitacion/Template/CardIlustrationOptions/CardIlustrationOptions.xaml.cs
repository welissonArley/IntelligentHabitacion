using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.Template.CardIlustrationOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardIlustrationOptions : ContentView
    {
        public ImageSource Ilustration
        {
            set
            {
                ImageIlustration.Source = value;
            }
        }

        public string TitleCard
        {
            set
            {
                LabelTitle.Text = value;
            }
        }

        public string DescriptionCard
        {
            set
            {
                LabelDescriptionCard.Text = value;
            }
        }

        public Thickness IlustrationMargin
        {
            set
            {
                ImageIlustration.Margin = value;
            }
        }

        public CardIlustrationOptions()
        {
            InitializeComponent();
        }
    }
}