using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Step
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompleteStep : ContentView
    {
        public bool RemoveMargin { get; set; }

        public static readonly BindableProperty RemoveMarginProperty = BindableProperty.Create(
                                                        propertyName: "RemoveMarginRight",
                                                        returnType: typeof(bool),
                                                        declaringType: typeof(CompleteStep),
                                                        defaultValue: false,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: RemoveMarginPropertyChanged);
        private static void RemoveMarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)newValue)
                ((CompleteStep)bindable).component.Margin = new Thickness(0);
        }

        public CompleteStep()
        {
            InitializeComponent();
        }
    }
}