using IntelligentHabitacion.App.Behavior;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.TextWithLabel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputTextWithLabelComponent : ContentView
    {
        public bool TopMargin { get; set; }

        public Xamarin.Forms.Behavior EntryBehavior
        {
            get => (Xamarin.Forms.Behavior)GetValue(EntryBehaviorProperty);
            set => SetValue(EntryBehaviorProperty, value);
        }

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

        public static BindableProperty EntryBehaviorProperty = BindableProperty.Create(
                                                        propertyName: "EntryBehavior",
                                                        returnType: typeof(Xamarin.Forms.Behavior),
                                                        declaringType: typeof(InputTextWithLabelComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: EntryBehaviorPropertyChanged);

        private static void EntryBehaviorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
                ((InputTextWithLabelComponent)bindable).Input.Behaviors.Add((Xamarin.Forms.Behavior)newValue);
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