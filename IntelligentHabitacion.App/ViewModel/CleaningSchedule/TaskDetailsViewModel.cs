using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.Calendar;
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
        private readonly Lazy<ICalendarUseCase> useCase;
        private ICalendarUseCase _useCase => useCase.Value;

        public TaskModel TaskModel { get; set; }
        public CleaningScheduleCalendarModel Model { get; set; }

        public ICommand OnDateChangedCommand { get; }

        public TaskDetailsViewModel(Lazy<ICalendarUseCase> useCase)
        {
            CurrentState = LayoutState.Loading;

            this.useCase = useCase;

            OnDateChangedCommand = new Command(async (dateReturned) =>
            {
                CurrentState = LayoutState.Loading;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));

                var date = (DateTime)dateReturned;

                Model = await _useCase.Execute(date, TaskModel.Room);

                CurrentState = LayoutState.None;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            });
        }

        public async Task Initialize(TaskModel taskModel)
        {
            TaskModel = taskModel;

            Model = await _useCase.Execute(DateTime.UtcNow, taskModel.Room);

            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
    }
}
