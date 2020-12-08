using IntelligentHabitacion.App.Model;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleCleanHomeTemplate : ContentView
    {
        public ScheduleModel ScheduleUser
        {
            get => (ScheduleModel)GetValue(ScheduleUserProperty);
            set => SetValue(ScheduleUserProperty, value);
        }

        public static readonly BindableProperty ScheduleUserProperty = BindableProperty.Create(
                                                        propertyName: "ScheduleUser",
                                                        returnType: typeof(ScheduleModel),
                                                        declaringType: typeof(ScheduleCleanHomeTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: ScheduleUserChanged);

        private static void ScheduleUserChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var scheduleModel = (ScheduleModel)newValue;
                var component = ((ScheduleCleanHomeTemplate)bindable);
                component.RoomLabel.Text = scheduleModel.Room;

                var dayofWeek = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(scheduleModel.Date.DayOfWeek);
                component.DateLabel.Text = $"{dayofWeek.First().ToString().ToUpper() + dayofWeek.Substring(1)} • {scheduleModel.Date.Day} {scheduleModel.Date:MMM} {scheduleModel.Date.Year}";
                if (scheduleModel.CanBeRate)
                {
                    component.ButtonToRate.IsVisible = true;
                    component.SeeDetailsLabel.IsVisible = false;
                }
                else
                {
                    component.ButtonToRate.IsVisible = false;
                    component.SeeDetailsLabel.IsVisible = true;
                }

                if(scheduleModel.RatingStars == -1)
                {
                    component.LabelWithouRate.IsVisible = true;
                    component.RatingStars.IsVisible = false;
                }
                else
                {
                    component.LabelWithouRate.IsVisible = false;
                    component.RatingStars.IsVisible = true;
                    component.RatingStars.Rating = scheduleModel.RatingStars;
                }
            }
        }

        public ScheduleCleanHomeTemplate()
        {
            InitializeComponent();
        }
    }
}