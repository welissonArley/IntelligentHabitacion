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

                component.Content.Children.Clear();

                if (taskModel.Tasks.Any())
                {
                    foreach (var task in taskModel.Tasks)
                        component.Content.Children.Add(ComponentRoom(task.Room));

                    component.ButtonChange.IsVisible = true;
                }
                else
                {
                    component.Content.Children.Add(ComponentWithoutTask(bindable));
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
    
        private static StackLayout ComponentWithoutTask(BindableObject bindable)
        {
            var button = new Xamarin.Forms.Button
            {
                TextColor = (Color)Application.Current.Resources["YellowDefault"],
                Text = ResourceText.TITLE_ASSIGN_TASKS,
                HorizontalOptions = LayoutOptions.End,
                CornerRadius = 0,
                BackgroundColor = Color.White,
                HeightRequest = 30
            };

            button.Clicked += (sender, args) =>
            {
                var component = ((ManageScheduleTemplate)bindable);
                component.TappedChangeTasksCommand?.Execute(component.UserTask.Id);
            };

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
                    button,
                    new BoxView
                    {
                        HeightRequest = 1,
                        BackgroundColor = (Color)Application.Current.Resources["GrayDefault"],
                        Opacity = 0.2
                    }
                }
            };
        }

        private void ButtonChange_Clicked(object sender, System.EventArgs e)
        {
            TappedChangeTasksCommand?.Execute(UserTask.Id);
        }
    }
}