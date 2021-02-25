using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhonenumberToCallComponent : ContentView
    {
        public string TitleLabel
        {
            get => (string)GetValue(TitleLabelProperty);
            set => SetValue(TitleLabelProperty, value);
        }
        public string PhoneNumber
        {
            get => (string)GetValue(PhoneNumberProperty);
            set => SetValue(PhoneNumberProperty, value);
        }
        public ICommand TappedMakePhonecallCommand
        {
            get => (ICommand)GetValue(TappedMakePhonecallCommandProperty);
            set => SetValue(TappedMakePhonecallCommandProperty, value);
        }
        public string ProfileColor
        {
            get => (string)GetValue(ProfileColorProperty);
            set => SetValue(ProfileColorProperty, value);
        }

        public static readonly BindableProperty TappedMakePhonecallCommandProperty = BindableProperty.Create(propertyName: "TappedMakePhonecall",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(PhonenumberToCallComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty ProfileColorProperty = BindableProperty.Create(propertyName: "ProfileColor",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(PhonenumberToCallComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: ProfileColorPropertyChanged);

        public static readonly BindableProperty PhoneNumberProperty = BindableProperty.Create(propertyName: "PhoneNumber",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(PhonenumberToCallComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: PhoneNumberPropertyChanged);

        public static readonly BindableProperty TitleLabelProperty = BindableProperty.Create(propertyName: "TitleLabel",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(PhonenumberToCallComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: TitleLabelPropertyChanged);

        private static void ProfileColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PhonenumberToCallComponent)bindable).BackgroundIlustrationPhone.Fill = new SolidColorBrush(Color.FromHex(newValue.ToString()));
        }
        private static void PhoneNumberPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PhonenumberToCallComponent)bindable).LabelPhoneNumber.Text = newValue.ToString();
        }
        private static void TitleLabelPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PhonenumberToCallComponent)bindable).LabelTitle.Text = newValue.ToString();
        }

        public PhonenumberToCallComponent()
        {
            InitializeComponent();
        }

        private void MakePhoneCall_Tapped(object sender, System.EventArgs e)
        {
            TappedMakePhonecallCommand?.Execute(PhoneNumber);
        }
    }
}