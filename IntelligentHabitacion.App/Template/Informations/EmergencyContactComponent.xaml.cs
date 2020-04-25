using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmergencyContactComponent : ContentView
    {
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
        public string ContactName
        {
            get => (string)GetValue(ProfileColorProperty);
            set => SetValue(ProfileColorProperty, value);
        }
        public string Relationship
        {
            get => (string)GetValue(RelationshipProperty);
            set => SetValue(RelationshipProperty, value);
        }

        public static readonly BindableProperty TappedMakePhonecallCommandProperty = BindableProperty.Create(propertyName: "TappedMakePhonecall",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(EmergencyContactComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: TappedMakePhonecallCommandChanged);

        public static readonly BindableProperty ProfileColorProperty = BindableProperty.Create(propertyName: "ProfileColor",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(EmergencyContactComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: ProfileColorPropertyChanged);

        public static readonly BindableProperty PhoneNumberProperty = BindableProperty.Create(propertyName: "PhoneNumber",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(EmergencyContactComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: PhoneNumberPropertyChanged);

        public static readonly BindableProperty ContactNameProperty = BindableProperty.Create(propertyName: "ContactName",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(EmergencyContactComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: ContactNameChanged);

        public static readonly BindableProperty RelationshipProperty = BindableProperty.Create(propertyName: "Relationship",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(EmergencyContactComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: RelationshipChanged);

        private static void ProfileColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((EmergencyContactComponent)bindable).ComponentPhonenumberToCall.ProfileColor = newValue.ToString();
        }
        private static void PhoneNumberPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((EmergencyContactComponent)bindable).ComponentPhonenumberToCall.PhoneNumber = newValue.ToString();
        }
        private static void TappedMakePhonecallCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((EmergencyContactComponent)bindable).ComponentPhonenumberToCall.TappedMakePhonecallCommand = (ICommand)newValue;
        }
        private static void ContactNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((EmergencyContactComponent)bindable).LabelName.Text = newValue.ToString();
        }
        private static void RelationshipChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((EmergencyContactComponent)bindable).LabelRelationship.Text = newValue.ToString();
        }

        public EmergencyContactComponent()
        {
            InitializeComponent();
        }
    }
}