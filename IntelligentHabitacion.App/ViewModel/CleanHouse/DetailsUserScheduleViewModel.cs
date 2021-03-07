using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class DetailsUserScheduleViewModel : BaseViewModel
    {
        private ICleaningScheduleRule _rule { get; set; }

        public string UserId { get; set; }
        public DetailsUserScheduleModel Model { get; set; }

        public ICommand SeeDetailsRatingCommand { get; private set; }
        public ICommand RatingFriendCommand { get; private set; }
        public ICommand MonthChangedCommand { get; private set; }

        public DetailsUserScheduleViewModel(ICleaningScheduleRule rule)
        {
            _rule = rule;

            RatingFriendCommand = new Command(async (taskForTheMonth) => { await RatingFriendTask((TaskForTheMonthDetails)taskForTheMonth); });
            SeeDetailsRatingCommand = new Command(async () => { await SeeDatailsRatingTask(); });
            MonthChangedCommand = new Command(async (date) => await GetDetails((DateTime)date));
        }

        private async Task RatingFriendTask(TaskForTheMonthDetails taskForTheMonth)
        {
            try
            {
                await ShowLoading();

                var tempList = Model.Tasks.Select(c => c.IndexOf(taskForTheMonth)).ToList();
                var index = tempList.IndexOf(tempList.First(c => c != -1));

                await Navigation.PushAsync<RatingCleaningViewModel>((viewModel, page) =>
                {
                    viewModel.Model = new RatingCleaningModel
                    {
                        Id = taskForTheMonth.Id,
                        Date = Model.Tasks.ElementAt(index).Date,
                        Name = Model.Name,
                        Room = taskForTheMonth.Room
                    };
                    viewModel.CallbackOnConcludeCommand = new Command((response) => { CallbackRatingFriendTask(response as TaskForTheMonthDetails); });
                });
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task SeeDatailsRatingTask()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<SeeDetailsRatingCleaningViewModel>();
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private void CallbackRatingFriendTask(TaskForTheMonthDetails response)
        {
            var task = Model.Tasks.SelectMany(c => c).First(c => c.Id.Equals(response.Id));
            var taskGroup = Model.Tasks.Where(c => c.Contains(task)).First();

            var index = Model.Tasks.IndexOf(taskGroup);

            Model.Tasks.RemoveAt(index);

            var newList = taskGroup.Where(c => !c.Id.Equals(response.Id)).Select(c => new TaskForTheMonthDetails
            {
                Id = c.Id,
                CanBeRate = c.CanBeRate,
                RatingStars = c.RatingStars,
                Room = c.Room
            }).ToList();

            newList.Insert(0, response);

            Model.Tasks.Insert(index, new TaskForTheMonthDetailsGroup(taskGroup.Date, new ObservableCollection<TaskForTheMonthDetails>(newList)));

            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
        }

        private async Task GetDetails(DateTime date)
        {
            try
            {
                await ShowLoading();

                var response = await _rule.GetDetailsAllTasksUserForAMonth(UserId, date);

                Model = response;

                OnPropertyChanged(new PropertyChangedEventArgs("Model"));

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
