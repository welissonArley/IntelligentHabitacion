using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Step
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Steps : ContentView
    {
        public int AmountCompleteSteps { get; set; }
        public int AmountIncompleteSteps { get; set; }

        public static readonly BindableProperty AmountCompleteStepsProperty = BindableProperty.Create(
                                                        propertyName: "AmountCompleteSteps",
                                                        returnType: typeof(int),
                                                        declaringType: typeof(Steps),
                                                        defaultValue: 0,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: AmountCompleteStepsPropertyChanged);

        public static readonly BindableProperty AmountIncompleteStepsProperty = BindableProperty.Create(
                                                        propertyName: "AmountIncompleteSteps",
                                                        returnType: typeof(int),
                                                        declaringType: typeof(Steps),
                                                        defaultValue: 0,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: AmountIncompleteStepsPropertyChanged);

        private static void AmountCompleteStepsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var amount = (int)newValue;
                var content = ((Steps)bindable).ContentSteps;

                for(var index = 0; index < amount; index++)
                {
                    content.Children.Insert(0, CreateEllipseCompleteStep());
                }
            }
        }
        private static void AmountIncompleteStepsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var amount = (int)newValue;
                var content = ((Steps)bindable).ContentSteps;

                for (var index = 0; index < amount; index++)
                {
                    content.Children.Add(CreateEllipseIncompleteStep());
                }
            }
        }

        public Steps()
        {
            InitializeComponent();
        }

        private static Shape CreateEllipseCompleteStep()
        {
            return new Ellipse
            {
                HeightRequest = 16,
                WidthRequest = 16,
                Fill = new SolidColorBrush(Color.FromHex("#FEBF3B"))
            };
        }
        private static Shape CreateEllipseIncompleteStep()
        {
            return new Ellipse
            {
                HeightRequest = 16,
                WidthRequest = 16,
                StrokeThickness = 3,
                Stroke = new SolidColorBrush(Color.Black)
            };
        }
    }
}