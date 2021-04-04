using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.Calendar;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleaningSchedule
{
    public class TaskDetailsViewModel : BaseViewModel
    {
        public LayoutState CurrentStateCalendar { get; set; }
        public LayoutState CurrentStateHistoric { get; set; }

        private readonly Lazy<ICalendarUseCase> useCase;
        private ICalendarUseCase _useCase => useCase.Value;

        public TaskModel TaskModel { get; set; }
        public CleaningScheduleCalendarModel Model { get; set; }
        public ObservableCollection<DetailsTaskCleanedOnDayModel> DetailsDayModel { get; set; }

        public ICommand OnDateChangedCommand { get; }
        public ICommand OnDaySelectedCommand { get; }

        public TaskDetailsViewModel(Lazy<ICalendarUseCase> useCase)
        {
            this.useCase = useCase;

            OnDateChangedCommand = new Command(async (dateReturned) =>
            {
                var date = (DateTime)dateReturned;

                await FillCalendarModel(date, TaskModel.Room);
                await FillTaskDayDetailsModel(date);
            });
            OnDaySelectedCommand = new Command(async (dateReturned) =>
            {
                var date = (DateTime)dateReturned;

                await FillTaskDayDetailsModel(date);
            });
        }

        private async Task FillCalendarModel(DateTime date, string room)
        {
            CurrentStateCalendar = LayoutState.Loading;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));

            Model = await _useCase.Execute(date, room);

            CurrentStateCalendar = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));
        }
        private async Task FillTaskDayDetailsModel(DateTime date)
        {
            CurrentStateHistoric = LayoutState.Loading;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));

            CurrentStateHistoric = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
        }

        public async Task Initialize(TaskModel taskModel)
        {
            TaskModel = taskModel;

            var calendarTask = FillCalendarModel(DateTime.UtcNow, taskModel.Room);
            var taskDetailsTask = FillTaskDayDetailsModel(DateTime.UtcNow);

            await Task.WhenAll(calendarTask, taskDetailsTask);
        }
    }
}
