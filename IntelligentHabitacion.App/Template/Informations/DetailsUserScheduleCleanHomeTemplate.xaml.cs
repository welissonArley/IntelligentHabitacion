using IntelligentHabitacion.App.Model;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsUserScheduleCleanHomeTemplate : ContentView
    {
        public TaskForTheMonthDetails ScheduleUser
        {
            get => (TaskForTheMonthDetails)GetValue(ScheduleUserProperty);
            set => SetValue(ScheduleUserProperty, value);
        }

        public ICommand RateCommand
        {
            get => (ICommand)GetValue(RateCommandProperty);
            set => SetValue(RateCommandProperty, value);
        }

        public ICommand SeeDetailsCommand
        {
            get => (ICommand)GetValue(SeeDetailsCommandProperty);
            set => SetValue(SeeDetailsCommandProperty, value);
        }

        public static readonly BindableProperty ScheduleUserProperty = BindableProperty.Create(
                                                        propertyName: "ScheduleUser",
                                                        returnType: typeof(TaskForTheMonthDetails),
                                                        declaringType: typeof(DetailsUserScheduleCleanHomeTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: ScheduleUserChanged);

        public static readonly BindableProperty SeeDetailsCommandProperty = BindableProperty.Create(
                                                        propertyName: "SeeDetails",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(DetailsUserScheduleCleanHomeTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty RateCommandProperty = BindableProperty.Create(
                                                        propertyName: "Rate",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(DetailsUserScheduleCleanHomeTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: null);

        private static void ScheduleUserChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var scheduleModel = (TaskForTheMonthDetails)newValue;
                var component = ((DetailsUserScheduleCleanHomeTemplate)bindable);
                component.RoomLabel.Text = scheduleModel.Room;

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
                    component.LabelWithoutRate.IsVisible = true;
                    component.RatingStars.IsVisible = false;
                    component.SeeDetailsLabel.IsVisible = false;
                }
                else
                {
                    component.LabelWithoutRate.IsVisible = false;
                    component.RatingStars.IsVisible = true;
                    component.RatingStars.Rating = scheduleModel.RatingStars;
                }
            }
        }
        public DetailsUserScheduleCleanHomeTemplate()
        {
            InitializeComponent();
        }

        private void ButtonToRate_Clicked(object sender, System.EventArgs e)
        {
            RateCommand?.Execute(ScheduleUser);
        }

        private void SeeDetailsLabel_Clicked(object sender, System.EventArgs e)
        {
            SeeDetailsCommand?.Execute(ScheduleUser);
        }
    }
}