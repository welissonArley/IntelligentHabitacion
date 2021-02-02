using IntelligentHabitacion.App.View.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Date
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthSelectorTemplate : ContentView
    {
        public DateTime Month
        {
            get => (DateTime)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }

        public static readonly BindableProperty MonthProperty = BindableProperty.Create(
                                                        propertyName: "Month",
                                                        returnType: typeof(DateTime),
                                                        declaringType: typeof(MonthSelectorTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: MonthChanged);

        private static void MonthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var date = (DateTime)newValue;
                var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);

                var component = ((MonthSelectorTemplate)bindable);
                component.LabelMonth.Text = $"{month.First().ToString().ToUpper() + month.Substring(1)}, {date.Year}";
            }
        }

        public MonthSelectorTemplate()
        {
            InitializeComponent();
        }

        private void MonthSelector_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushPopupAsync(new CalendarMonth());
        }
    }
}