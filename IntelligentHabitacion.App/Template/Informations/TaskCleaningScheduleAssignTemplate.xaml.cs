using IntelligentHabitacion.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskCleaningScheduleAssignTemplate : ContentView
    {
        public TaskModel Task
        {
            get => (TaskModel)GetValue(TaskProperty);
            set => SetValue(TaskProperty, value);
        }
        public static readonly BindableProperty TaskProperty = BindableProperty.Create(
                                                        propertyName: "Task",
                                                        returnType: typeof(TaskModel),
                                                        declaringType: typeof(TaskCleaningScheduleAssignTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TaskChanged);

        private static void TaskChanged(BindableObject bindable, object oldValue, object newValue)
        {
            newValue = oldValue != null && newValue == null ? oldValue : newValue;

            if (newValue != null)
            {
                var taskModel = (TaskModel)newValue;
                var component = ((TaskCleaningScheduleAssignTemplate)bindable);

                for(var index = taskModel.Assign.Count; index > 0; index--)
                {
                    var assign = taskModel.Assign.ElementAt(index-1);
                    component.ContentAssign.Children.Insert(0, CreateEllipseAssign(assign.ProfileColor, assign.Name));
                }

                component.Room.Text = taskModel.Room;
                component.OptionEdit.IsVisible = taskModel.CanEdit;
                component.ThereIsTaskToRateContent.IsVisible = taskModel.CanRate;
                component.CompletedTodayContent.IsVisible = taskModel.CanCompletedToday;
                if (taskModel.CanCompletedToday)
                {
                    component.CompletedTodayContent.CheckChangedCommand = new Command((value) =>
                    {
                        component.CompletedTodayContent.IsVisible = false;
                    });
                }
            }
        }

        private static Grid CreateEllipseAssign(string color, string name)
        {
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.End,
                RowDefinitions =
                {
                    new RowDefinition
                    {
                        Height = 36
                    }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition
                    {
                        Width = 36
                    }
                }
            };

            grid.Children.Add(new Ellipse
            {
                Fill = new SolidColorBrush(Color.FromHex(color)),
                HeightRequest = 36,
                WidthRequest = 36
            }, 0, 0);
            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = new Useful.ShortNameConverter().Converter(name),
                TextColor = Color.White,
                FontSize = 14,
                Style = (Style)Application.Current.Resources["LabelBold"]
            }, 0, 0);

            return grid;
        }

        public TaskCleaningScheduleAssignTemplate()
        {
            InitializeComponent();
        }
    }
}