using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CleanHomeViewMonthScheduleTemplate : ContentView
    {
        public FriendSchedule Schedule
        {
            get => (FriendSchedule)GetValue(ScheduleProperty);
            set => SetValue(ScheduleProperty, value);
        }

        public static readonly BindableProperty ScheduleProperty = BindableProperty.Create(
                                                        propertyName: "Schedule",
                                                        returnType: typeof(FriendSchedule),
                                                        declaringType: typeof(CleanHomeViewMonthScheduleTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: ScheduleChanged);

        private static void ScheduleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var scheduleModel = (FriendSchedule)newValue;
                var component = ((CleanHomeViewMonthScheduleTemplate)bindable);
                component.FriendNameLabel.Text = scheduleModel.Name;

                if (string.IsNullOrWhiteSpace(scheduleModel.Room))
                {
                    component.ContentWithoutTasks.IsVisible = true;
                    component.ContentWithTasks.IsVisible = false;
                     
                    var userPreferences = Resolver.Resolve<UserPreferences>();

                    if (userPreferences.IsAdministrator)
                    {
                        component.ButtonAssignTask.IsVisible = true;
                        component.FrameNoTask.IsVisible = false;
                    }
                    else
                    {
                        component.ButtonAssignTask.IsVisible = false;
                        component.FrameNoTask.IsVisible = true;
                    }
                }
                else
                {
                    component.ContentWithoutTasks.IsVisible = false;
                    component.ContentWithTasks.IsVisible = true;

                    component.RoomLabel.Text = scheduleModel.Room;
                    component.DetailButton.TextColor = Color.FromHex(scheduleModel.ProfileColor);

                    foreach (var day in scheduleModel.DaysOfTheMonthTheFriendCleanedTheRoom)
                        component.Days.Children.Add(DayTemplate(day, scheduleModel.ProfileColor));
                }
            }
        }

        private static Grid DayTemplate(int day, string color)
        {
            var grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = 15 }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 15 }
                }
            };

            grid.Children.Add(new RoundedBoxView.Forms.Plugin.Abstractions.RoundedBoxView
            {
                HeightRequest = 15,
                WidthRequest = 15,
                BackgroundColor = Color.White,
                CornerRadius = 7,
                BorderColor = Color.FromHex(color),
                BorderThickness = 1
            });

            grid.Children.Add(new Label
            {
                Text = day < 10 ? $"0{day}" : $"{day}",
                TextColor = Color.FromHex(color),
                FontSize = 8,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            });

            return grid;
        }

        public CleanHomeViewMonthScheduleTemplate()
        {
            InitializeComponent();
        }
    }
}