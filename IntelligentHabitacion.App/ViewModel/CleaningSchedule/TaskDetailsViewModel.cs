using IntelligentHabitacion.App.Model;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleaningSchedule
{
    public class TaskDetailsViewModel : BaseViewModel
    {
        public TaskModel TaskModel { get; set; }
        public CleaningScheduleCalendarModel Model { get; set; }

        public ICommand OnDateChangedCommand { get; }

        public TaskDetailsViewModel()
        {
            CurrentState = LayoutState.Loading;

            OnDateChangedCommand = new Command((dateReturned) =>
            {
                var date = (DateTime)dateReturned;
            });
        }

        public async Task Initialize(TaskModel taskModel)
        {
            TaskModel = taskModel;

            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
    }
}
