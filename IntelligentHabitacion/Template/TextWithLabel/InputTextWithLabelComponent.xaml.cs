using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.Template.TextWithLabel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputTextWithLabelComponent : ContentView
    {
        public bool TopMargin { get; set; }

        public static BindableProperty TopMarginProperty = BindableProperty.Create(
                                                        propertyName: "TopMargin",
                                                        returnType: typeof(bool),
                                                        declaringType: typeof(InputTextWithLabelComponent),
                                                        defaultValue: false,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TopMarginPropertyChanged);
        private static void TopMarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)newValue)
                ((InputTextWithLabelComponent)bindable).component.Margin = new Thickness(0, 20, 0, 0);
        }

        public string PropertyToBindindEntry { set { Input.SetBinding(Entry.TextProperty, value); } }
        public string LabelTitle { get; set; }
        public string PlaceHolderText { set { Input.Placeholder = value; } }
        public Keyboard Keyboard { set { Input.Keyboard = value; } }
        public bool IsPassword { set { Input.IsPassword = value; } }

        public InputTextWithLabelComponent()
        {
            InitializeComponent();

            InputTextChanged();
        }

        private void InputTextChanged()
        {
            Input.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(Input.Text))
                    Label.Text = " ";
                else
                    Label.Text = LabelTitle;
            };
        }
    }
}