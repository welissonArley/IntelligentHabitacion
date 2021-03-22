using IntelligentHabitacion.App.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmergencyContactsComponent : ContentView
    {
        public string ProfileColor
        {
            get => (string)GetValue(ProfileColorProperty);
            set => SetValue(ProfileColorProperty, value);
        }
        public ICommand TappedMakePhonecallCommand
        {
            get => (ICommand)GetValue(TappedMakePhonecallCommandProperty);
            set => SetValue(TappedMakePhonecallCommandProperty, value);
        }

        public IList<EmergencyContactModel> EmergencyContacts
        {
            get => (IList<EmergencyContactModel>)GetValue(EmergencyContactsProperty);
            set => SetValue(EmergencyContactsProperty, value);
        }

        public static readonly BindableProperty EmergencyContactsProperty = BindableProperty.Create(
                                                        propertyName: "EmergencyContacts",
                                                        returnType: typeof(IList<EmergencyContactModel>),
                                                        declaringType: typeof(EmergencyContactsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: EmergencyContactsChanged);

        public static readonly BindableProperty TappedMakePhonecallCommandProperty = BindableProperty.Create(propertyName: "TappedMakePhonecall",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(EmergencyContactsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty ProfileColorProperty = BindableProperty.Create(propertyName: "ProfileColor",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(EmergencyContactsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void EmergencyContactsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var emergencyContacts = (IList<EmergencyContactModel>)newValue;
                var component = ((EmergencyContactsComponent)bindable);

                SetGridDefinitions(component.Content);

                for (var index = 0; index < emergencyContacts.Count; index++)
                    component.InsertLayout(index, component.Content, emergencyContacts.ElementAt(index));
            }
        }

        public void InsertLayout(int index, Grid grid, EmergencyContactModel contact)
        {
            var imageButton = new ImageButton
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                BackgroundColor = Color.FromHex(string.IsNullOrEmpty(ProfileColor) ? "#000000" : ProfileColor),
                HeightRequest = 30,
                CornerRadius = 15,
                Margin = 15,
                Source = ImageSource.FromFile("IconPhone"),
                Padding = new Thickness(0, 7, 0, 7)
            };
            imageButton.Clicked += (obg, events) =>
            {
                TappedMakePhonecallCommand?.Execute(contact.PhoneNumber);
            };

            var frame = new Frame
            {
                HasShadow = true,
                CornerRadius = 15,
                Padding = 0
            };

            frame.Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = ResourceText.TITLE_NAME_TWOPOINTS,
                        Style = (Style)Application.Current.Resources["LabelThin"],
                        FontSize = 8,
                        Margin = new Thickness(15,20,0,0)
                    },
                    new Label
                    {
                        Text = contact.Name,
                        Style = (Style)Application.Current.Resources["LabelMedium"],
                        FontSize = 16,
                        Margin = new Thickness(15,0,0,0)
                    },
                    new Label
                    {
                        Text = ResourceText.PLACEHOLDER_RELATIONSHIP,
                        Style = (Style)Application.Current.Resources["LabelThin"],
                        FontSize = 8,
                        Margin = new Thickness(15,10,0,0)
                    },
                    new Label
                    {
                        Text = contact.Relationship,
                        Style = (Style)Application.Current.Resources["LabelMedium"],
                        FontSize = 14,
                        Margin = new Thickness(15,0,0,0)
                    },
                    new Label
                    {
                        Text = ResourceText.TITLE_PHONENUMBERS,
                        Style = (Style)Application.Current.Resources["LabelThin"],
                        FontSize = 8,
                        Margin = new Thickness(15,10,0,0)
                    },
                    new Label
                    {
                        Text = contact.PhoneNumber,
                        Style = (Style)Application.Current.Resources["LabelMedium"],
                        FontSize = 14,
                        Margin = new Thickness(15,0,0,0)
                    },
                    imageButton
                }
            };

            grid.Children.Add(frame, index, 0);
        }

        private static void SetGridDefinitions(Grid grid)
        {
            var cardHeight = ((IntelligentHabitacionDevice.IntelligentHabitacionDevice.Width() / 2) - 35) * 1.27;

            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
        }

        public EmergencyContactsComponent()
        {
            InitializeComponent();
        }
    }
}