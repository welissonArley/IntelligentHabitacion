using IntelligentHabitacion.App.Model;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageScheduleTemplate : ContentView
    {
        public AllFriendsGroup UserTask
        {
            get => (AllFriendsGroup)GetValue(UserTaskProperty);
            set => SetValue(UserTaskProperty, value);
        }

        public ICommand TappedChangeTasksCommand
        {
            get => (ICommand)GetValue(TappedChangeTasksCommandProperty);
            set => SetValue(TappedChangeTasksCommandProperty, value);
        }

        public static readonly BindableProperty UserTaskProperty = BindableProperty.Create(
                                                        propertyName: "UserTask",
                                                        returnType: typeof(AllFriendsGroup),
                                                        declaringType: typeof(ManageScheduleTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: FriendTaskChanged);

        public static readonly BindableProperty TappedChangeTasksCommandProperty = BindableProperty.Create(propertyName: "TappedChangeTasks",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(ManageScheduleTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void FriendTaskChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var taskModel = (AllFriendsGroup)newValue;
                var component = ((ManageScheduleTemplate)bindable);
                component.LabelName.Text = taskModel.Name;
                component.ButtonChange.TextColor = Color.FromHex(taskModel.ProfileColor);

                if (taskModel.Tasks.Any())
                {
                    foreach (var task in taskModel.Tasks)
                        component.Content.Children.Add(ComponentRoom(task.Room));
                }
                else
                {
                    component.Content.Children.Add(ComponentWithoutTask());
                    component.ButtonChange.IsVisible = false;
                }
            }
        }

        public ManageScheduleTemplate()
        {
            InitializeComponent();
        }

        private static StackLayout ComponentRoom(string room)
        {
            return new StackLayout
            {
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
                    new BoxView
                    {
                        HeightRequest = 1,
                        BackgroundColor = (Color)Application.Current.Resources["GrayDefault"],
                        Opacity = 0.2
                    }
                }
            };
        }
    
        private static StackLayout ComponentWithoutTask()
        {
            return new StackLayout
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
                    new Xamarin.Forms.Button
                    {
                        Style = (Style)Application.Current.Resources["ButtonYellowDefault"],
                        Text = ResourceText.TITLE_ASSIGN_TASKS,
                        CornerRadius = 0,
                        HeightRequest = 30
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

        private void ButtonDetails_Clicked(object sender, System.EventArgs e)
        {
            TappedChangeTasksCommand?.Execute(UserTask.Id);
        }
    }
}