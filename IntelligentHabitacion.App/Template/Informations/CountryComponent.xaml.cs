using IntelligentHabitacion.App.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryComponent : ContentView
    {
        public CountryModel Country
        {
            get => (CountryModel)GetValue(CountryProperty);
            set => SetValue(CountryProperty, value);
        }

        public static readonly BindableProperty CountryProperty = BindableProperty.Create(
                                                        propertyName: "Country",
                                                        returnType: typeof(CountryModel),
                                                        declaringType: typeof(CountryComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: CountryChanged);

        private static void CountryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var countryModel = (CountryModel)newValue;
                var component = ((CountryComponent)bindable);
                component.CountryPhoneCode.Text = countryModel.PhoneCode;
                component.CountryFlag.Source = ImageSource.FromUri(new Uri(countryModel.Flag));
                component.CountryName.Text = countryModel.Name;
            }
        }

        public CountryComponent()
        {
            InitializeComponent();
        }
    }
}