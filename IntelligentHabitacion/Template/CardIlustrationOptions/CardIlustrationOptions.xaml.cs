using System.Windows.Input;
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

        public static BindableProperty TappedCardCommandProperty = BindableProperty.Create(propertyName: "TappedCard",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(CardIlustrationOptions),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public ICommand TappedCardCommand
        {
            get => (ICommand)GetValue(TappedCardCommandProperty);
            set => SetValue(TappedCardCommandProperty, value);
        }

        public void Card_OnTapped(object sender, System.EventArgs e)
        {
            TappedCardCommand?.Execute(null);
        }

        public CardIlustrationOptions()
        {
            InitializeComponent();
        }
    }
}