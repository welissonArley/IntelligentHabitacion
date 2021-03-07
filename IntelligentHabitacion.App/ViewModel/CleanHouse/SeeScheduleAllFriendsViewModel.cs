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
    public class SeeScheduleAllFriendsViewModel : BaseViewModel
    {
        private readonly ICleaningScheduleRule _rule;

        public ICommand SearchTextChangedCommand { protected set; get; }
        public ICommand TappedSeeDetailsCommand { protected set; get; }

        public ICommand MonthChangedCommand { get; private set; }

        public AllFriendsTasksModel Model { get; set; }

        private ObservableCollection<AllFriendsGroup> _friendsTask { get; set; }

        public SeeScheduleAllFriendsViewModel(ICleaningScheduleRule rule)
        {
            _rule = rule;

            SearchTextChangedCommand = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });

            TappedSeeDetailsCommand = new Command(async (value) =>
            {
                await SeeFriendTasksDetails((string)value);
            });

            MonthChangedCommand = new Command(async (date) => await GetSchedule((DateTime)date));
        }

        private void OnSearchTextChanged(string value)
        {
            Model.FriendsTasks = new ObservableCollection<AllFriendsGroup>(_friendsTask.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
        }

        private async Task SeeFriendTasksDetails(string id)
        {
            try
            {
                await ShowLoading();
                var response = await _rule.GetDetailsAllTasksUserForAMonth(id, Model.Month);
                await Navigation.PushAsync<DetailsUserScheduleViewModel>((viewModel, page) =>
                {
                    viewModel.Model = response;
                    viewModel.UserId = id;
                });
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        public void FriendsTasksToSearch()
        {
            _friendsTask = Model.FriendsTasks;
        }

        private async Task GetSchedule(DateTime date)
        {
            try
            {
                await ShowLoading();

                Model = await _rule.GetFriendsTasks(date);

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
