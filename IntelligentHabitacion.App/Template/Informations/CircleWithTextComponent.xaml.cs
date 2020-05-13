using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircleWithTextComponent : ContentView
    {
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Color
        {
            get => (string)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                                                        propertyName: "Text",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CircleWithTextComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: TitleChanged);

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
                                                        propertyName: "Color",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CircleWithTextComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: ColorChanged);
        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var component = ((CircleWithTextComponent)bindable);
                component.LabelText.Text = newValue.ToString();
            }
        }
        private static void ColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var component = ((CircleWithTextComponent)bindable);
                component.CircleBoxView.BackgroundColor = Xamarin.Forms.Color.FromHex(newValue.ToString());
            }
        }

        public CircleWithTextComponent()
        {
            InitializeComponent();
        }
    }
}