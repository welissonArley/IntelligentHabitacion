using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.Calendar;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.HistoryOfTheDay;
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

        private readonly Lazy<IHistoryOfTheDayUseCase> historyOfTheDayUseCase;
        private readonly Lazy<ICalendarUseCase> useCase;
        private ICalendarUseCase _useCase => useCase.Value;
        private IHistoryOfTheDayUseCase _historyOfTheDayUseCase => historyOfTheDayUseCase.Value;

        public TaskModel TaskModel { get; set; }
        public CleaningScheduleCalendarModel Model { get; set; }
        public ObservableCollection<DetailsTaskCleanedOnDayModel> DetailsDayModel { get; set; }

        public ICommand OnDateChangedCommand { get; }
        public ICommand OnDaySelectedCommand { get; }

        public TaskDetailsViewModel(Lazy<ICalendarUseCase> useCase, Lazy<IHistoryOfTheDayUseCase> historyOfTheDayUseCase)
        {
            CurrentStateHistoric = LayoutState.Loading;
            CurrentStateCalendar = LayoutState.Loading;

            this.useCase = useCase;
            this.historyOfTheDayUseCase = historyOfTheDayUseCase;

            OnDateChangedCommand = new Command(async (dateReturned) =>
            {
                CurrentStateHistoric = LayoutState.Loading;
                CurrentStateCalendar = LayoutState.Loading;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));

                var date = (DateTime)dateReturned;

                await FillCalendarModel(date, TaskModel.Room);
                await FillTaskDayDetailsModel(date, TaskModel.Room);
            });
            OnDaySelectedCommand = new Command(async (dateReturned) =>
            {
                var date = (DateTime)dateReturned;

                await FillTaskDayDetailsModel(date, TaskModel.Room);
            });
        }

        private async Task FillCalendarModel(DateTime date, string room)
        {
            if(CurrentStateCalendar != LayoutState.Loading)
            {
                CurrentStateCalendar = LayoutState.Loading;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));
            }

            Model = await _useCase.Execute(date, room);

            CurrentStateCalendar = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));
        }
        private async Task FillTaskDayDetailsModel(DateTime date, string room)
        {
            if(CurrentStateHistoric != LayoutState.Loading)
            {
                CurrentStateHistoric = LayoutState.Loading;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
            }

            var result = await _historyOfTheDayUseCase.Execute(date, room);
            DetailsDayModel = new ObservableCollection<DetailsTaskCleanedOnDayModel>(result);

            CurrentStateHistoric = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
        }

        public async Task Initialize(TaskModel taskModel)
        {
            TaskModel = taskModel;

            await FillCalendarModel(DateTime.UtcNow, taskModel.Room);
            await FillTaskDayDetailsModel(DateTime.UtcNow, taskModel.Room);
        }
    }
}
