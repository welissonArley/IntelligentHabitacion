using IntelligentHabitacion.App.Model;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleAllFriendsTemplate : ContentView
    {
        public AllFriendsGroup FriendTask
        {
            get => (AllFriendsGroup)GetValue(FriendTaskProperty);
            set => SetValue(FriendTaskProperty, value);
        }

        public ICommand TappedSeeDetailsCommand
        {
            get => (ICommand)GetValue(TappedSeeDetailsCommandProperty);
            set => SetValue(TappedSeeDetailsCommandProperty, value);
        }

        public static readonly BindableProperty FriendTaskProperty = BindableProperty.Create(
                                                        propertyName: "FriendTask",
                                                        returnType: typeof(AllFriendsGroup),
                                                        declaringType: typeof(ScheduleAllFriendsTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: FriendTaskChanged);

        public static readonly BindableProperty TappedSeeDetailsCommandProperty = BindableProperty.Create(propertyName: "TappedSeeDetails",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(ScheduleAllFriendsTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void FriendTaskChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var taskModel = (AllFriendsGroup)newValue;
                var component = ((ScheduleAllFriendsTemplate)bindable);
                component.LabelName.Text = taskModel.Name;
                component.ButtonDetails.TextColor = Color.FromHex(taskModel.ProfileColor);

                if (taskModel.Tasks.Any())
                {
                    foreach (var task in taskModel.Tasks)
                        component.Content.Children.Add(ComponentRoomTotal(task.Room, task.CleaningRecords));
                }
                else
                {
                    component.Content.Children.Add(ComponentWithoutTask(taskModel.CanAssignTasks));
                    component.ButtonDetails.IsVisible = false;
                }
            }
        }

        public ScheduleAllFriendsTemplate()
        {
            InitializeComponent();
        }

        private static StackLayout ComponentRoomTotal(string room, int cleaningRecords)
        {
            return new StackLayout
            {
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label
                            {
                                HorizontalOptions = LayoutOptions.StartAndExpand,
                                Text = room,
                                FontSize = 14,
                                TextColor = (Color)Application.Current.Resources["GrayDefault"],
                                Style = (Style)Application.Current.Resources["LabelMedium"]
                            },
                            new Label
                            {
                                HorizontalOptions = LayoutOptions.EndAndExpand,
                                Text = $"{string.Format(ResourceText.TITLE_TOTAL, cleaningRecords)}",
                                FontSize = 14,
                                Style = (Style)Application.Current.Resources["LabelMedium"]
                            }
                        }
                    },
                    new BoxView
                    {
                        HeightRequest = 1,
                        BackgroundColor = (Color)Application.Current.Resources["GrayDefault"],
                        Opacity = 0.2
                    }
                }
            };
        }
    
        private static StackLayout ComponentWithoutTask(bool canAssignTasks)
        {
            var stackLayout = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = ResourceText.TITLE_NO_TASKS_THIS_MONTH,
                        FontSize = 14,
                        TextColor = (Color)Application.Current.Resources["GrayDefault"],
                        Style = (Style)Application.Current.Resources["LabelBold"]
                    },
                    new BoxView
                    {
                        HeightRequest = 1,
                        BackgroundColor = (Color)Application.Current.Resources["GrayDefault"],
                        Opacity = 0.2
                    }
                }
            };

            if (canAssignTasks)
            {
                stackLayout.Children.Insert(1, new Xamarin.Forms.Button
                {
                    Style = (Style)Application.Current.Resources["ButtonYellowDefault"],
                    Text = ResourceText.TITLE_ASSIGN_TASKS,
                    CornerRadius = 0,
                    HeightRequest = 30
                });
            }

            return stackLayout;
        }

        private void ButtonDetails_Clicked(object sender, System.EventArgs e)
        {
            TappedSeeDetailsCommand?.Execute(FriendTask.Id);
        }
    }
}