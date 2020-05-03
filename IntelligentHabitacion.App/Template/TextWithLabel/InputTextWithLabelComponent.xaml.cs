using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.TextWithLabel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputTextWithLabelComponent : ContentView
    {
        public bool TopMargin { get; set; }
        public IValueConverter ConverterToEntry { get; set; }

        public Xamarin.Forms.Behavior EntryBehavior
        {
            get => (Xamarin.Forms.Behavior)GetValue(EntryBehaviorProperty);
            set => SetValue(EntryBehaviorProperty, value);
        }

        public System.EventHandler<FocusEventArgs> EntryUnfocused
        {
            get => (System.EventHandler<FocusEventArgs>)GetValue(EntryUnfocusedProperty);
            set => SetValue(EntryUnfocusedProperty, value);
        }

        public static readonly BindableProperty TopMarginProperty = BindableProperty.Create(
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

        public static readonly BindableProperty EntryBehaviorProperty = BindableProperty.Create(
                                                        propertyName: "EntryBehavior",
                                                        returnType: typeof(Xamarin.Forms.Behavior),
                                                        declaringType: typeof(InputTextWithLabelComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: EntryBehaviorPropertyChanged);

        public static readonly BindableProperty EntryUnfocusedProperty = BindableProperty.Create(
                                                        propertyName: "EntryUnfocused",
                                                        returnType: typeof(System.EventHandler<FocusEventArgs>),
                                                        declaringType: typeof(InputTextWithLabelComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: EntryUnfocusedPropertyChanged);

        public static readonly BindableProperty PropertyToBindindEntryProperty = BindableProperty.Create(
                                                        propertyName: "PropertyToBindindEntry",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(InputTextWithLabelComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: PropertyToBindindEntryChanged);

        private static void EntryBehaviorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
                ((InputTextWithLabelComponent)bindable).Input.Behaviors.Add((Xamarin.Forms.Behavior)newValue);
        }

        private static void EntryUnfocusedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
                ((InputTextWithLabelComponent)bindable).Input.Unfocused += (System.EventHandler<FocusEventArgs>)newValue;
        }

        private static void PropertyToBindindEntryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Binding binding = new Binding(newValue.ToString())
            {
                Mode = BindingMode.TwoWay
            };

            var bindableComponent = ((InputTextWithLabelComponent)bindable);

            if (bindableComponent.ConverterToEntry != null)
                binding.Converter = bindableComponent.ConverterToEntry;

            bindableComponent.Input.SetBinding(Entry.TextProperty, binding);
        }

        public string PropertyToBindindEntry
        {
            get => (string)GetValue(PropertyToBindindEntryProperty);
            set => SetValue(PropertyToBindindEntryProperty, value);
        }
        public string LabelTitle { get; set; }
        public string PlaceHolderText { set { Input.Placeholder = value; } get { return Input.Placeholder; } }
        public Keyboard Keyboard { set { Input.Keyboard = value; } get { return Input.Keyboard; } }
        public bool IsPassword { set { Input.IsPassword = value; } get { return Input.IsPassword; } }

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