using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Date
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColumnDayTemplete : ContentView
    {
        private readonly Action<int> _callbackDaySelected;

        public ColumnDayTemplete(int day, Action<int> callbackDaySelected, bool dayUnavailable, bool selected = false)
        {
            InitializeComponent();
            _callbackDaySelected = callbackDaySelected;
            LabelDay.Text = $"{day}";

            if (dayUnavailable)
                DayUnavailable();
            else if (selected)
                DaySelected();
        }

        private void DayUnavailable()
        {
            DayContent.IsEnabled = false;
            DayContent.Opacity = 0.1;
        }

        private void SelectedDay(object sender, EventArgs e)
        {
            DaySelected();
            _callbackDaySelected(int.Parse(LabelDay.Text));
        }

        public void DeselectDay()
        {
            SelectComponent.IsVisible = false;
            LabelDay.TextColor = Color.Black;
        }

        private void DaySelected()
        {
            SelectComponent.IsVisible = true;
            LabelDay.TextColor = Color.White;
        }
    }
}