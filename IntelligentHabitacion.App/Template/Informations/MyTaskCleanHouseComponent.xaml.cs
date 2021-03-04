using IntelligentHabitacion.App.Model;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTaskCleanHouseComponent : ContentView
    {
        public TasksForTheMonth Task
        {
            get => (TasksForTheMonth)GetValue(TaskProperty);
            set => SetValue(TaskProperty, value);
        }

        public ICommand ConcludeTodayCommand
        {
            get => (ICommand)GetValue(ConcludeTodayCommandProperty);
            set => SetValue(ConcludeTodayCommandProperty, value);
        }

        public static readonly BindableProperty TaskProperty = BindableProperty.Create(
                                                        propertyName: "Task",
                                                        returnType: typeof(TasksForTheMonth),
                                                        declaringType: typeof(MyTaskCleanHouseComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TaskChanged);

        public static readonly BindableProperty ConcludeTodayCommandProperty = BindableProperty.Create(propertyName: "ConcludeToday",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(MyTaskCleanHouseComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void TaskChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var model = (TasksForTheMonth)newValue;
                var component = ((MyTaskCleanHouseComponent)bindable);
                component.LabelRoom.Text = model.Room;
                component.LabelRecord.Text = $"{string.Format(ResourceText.TITLE_CLEANING_RECORDS, model.CleaningRecords)}";
                component.LabelLastRecord.Text = model.LastRecord.HasValue ? $"{string.Format(ResourceText.TITLE_LAST_RECORD, model.LastRecord.Value.ToString(ResourceText.FORMAT_DATE))}" : "-";
            }
        }

        public MyTaskCleanHouseComponent()
        {
            InitializeComponent();
        }

        private void CompletedToday_Clicked(object sender, System.EventArgs e)
        {
            //ConcludeTodayCommand?.Execute(Task.Id);
        }
    }
}