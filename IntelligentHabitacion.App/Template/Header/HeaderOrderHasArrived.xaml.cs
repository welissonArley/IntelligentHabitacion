using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderOrderHasArrived : ContentView
    {
        public ICommand ButtonClickedCommand
        {
            get => (ICommand)GetValue(ButtonClickedCommandProperty);
            set => SetValue(ButtonClickedCommandProperty, value);
        }

        public static readonly BindableProperty ButtonClickedCommandProperty = BindableProperty.Create(propertyName: "ButtonClicked",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(HeaderOrderHasArrived),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public HeaderOrderHasArrived()
        {
            InitializeComponent();

            var deviceWidth = IntelligentHabitacionDevice.IntelligentHabitacionDevice.Width();

            ImageOrderHasArrived.WidthRequest = deviceWidth * 0.60;
            ImageOrderHasArrived.HeightRequest = ImageOrderHasArrived.WidthRequest * 1.20;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            ButtonClickedCommand?.Execute(null);
        }
    }
}