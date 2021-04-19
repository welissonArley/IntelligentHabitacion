using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.Calendar;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.HistoryOfTheDay;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        public ObservableCollection<DetailsTaskCleanedOnDayModelGroup> DetailsDayModel { get; set; }

        public ICommand OnDateChangedCommand { get; }
        public ICommand OnDaySelectedCommand { get; }
        public ICommand OnRateTaskTappedCommand { get; }
        public ICommand OnSeeDetailsRateTappedCommand { get; }

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
            OnRateTaskTappedCommand = new Command(async(taskToRate) =>
            {
                var taskToRateModel = (DetailsTaskCleanedOnDayModel)taskToRate;

                await Navigation.PushAsync<RateTaskViewModel>((viewModel, _) =>
                {
                    viewModel.Initialize(taskToRateModel, TaskModel.Room, new Command((newAverageRate) =>
                    {
                        CurrentStateCalendar = LayoutState.Loading;
                        CurrentStateHistoric = LayoutState.Loading;
                        OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
                        OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));

                        taskToRateModel.CanRate = false;
                        taskToRateModel.AverageRate = (int)newAverageRate;
                        Model.Date = taskToRateModel.CleanedAt;
                        Model.CleanedDays.First(c => c.Day == taskToRateModel.CleanedAt.Day).AmountcleanedRecordsToRate--;

                        CurrentStateHistoric = LayoutState.None;
                        CurrentStateCalendar = LayoutState.None;
                        OnPropertyChanged(new PropertyChangedEventArgs("DetailsDayModel"));
                        OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                        OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
                        OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));
                    }));
                });
            });
            OnSeeDetailsRateTappedCommand = new Command(async (taskToRate) =>
            {
                var taskToRateModel = (DetailsTaskCleanedOnDayModel)taskToRate;

                await Navigation.PushAsync<DetailsAllRateViewModel>(async (viewModel, _) =>
                {
                    await viewModel.Initialize(taskToRateModel.Id);
                });
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
            DetailsDayModel = new ObservableCollection<DetailsTaskCleanedOnDayModelGroup>(result);

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
