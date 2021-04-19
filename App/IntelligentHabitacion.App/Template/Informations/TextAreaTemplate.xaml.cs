using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextAreaTemplate : ContentView
    {
        public string PlaceHolderText { set => Input.Placeholder = value; }
        public string PropertyToBindindEntry
        {
            get => (string)GetValue(PropertyToBindindEntryProperty);
            set => SetValue(PropertyToBindindEntryProperty, value);
        }

        public static readonly BindableProperty PropertyToBindindEntryProperty = BindableProperty.Create(
                                                        propertyName: "PropertyToBindindEntry",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(TextAreaTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: PropertyToBindindEntryChanged);

        private static void PropertyToBindindEntryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Binding binding = new Binding(newValue.ToString())
            {
                Mode = BindingMode.TwoWay
            };

            var bindableComponent = ((TextAreaTemplate)bindable);

            bindableComponent.Input.SetBinding(Entry.TextProperty, binding);
        }

        public TextAreaTemplate()
        {
            InitializeComponent();

            InputTextChanged();
        }

        private void InputTextChanged()
        {
            Input.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(Input.Text))
                    LabelCount.Text = "0/255";
                else
                {
                    if (Input.Text.Length > 255)
                        Input.Text = Input.Text.Substring(0, 255);

                    LabelCount.Text = $"{Input.Text.Length}/{255}";
                }
            };
        }
    }
}