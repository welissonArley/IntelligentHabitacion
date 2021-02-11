using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.View.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class MyTasksViewModel : BaseViewModel
    {
        public bool ScheduleCreated { get; set; }
        public string ProfileColor { get; set; }
        public MyTasksCleanHouseModel Model { get; set; }

        public ICommand SeeFriendsTaskCommand { get; private set; }
        public ICommand SeeDetailsMyTasksCommand { get; private set; }
        public ICommand CompletedTodayTaskCommand { get; private set; }
        public ICommand CreateScheduleCommand { get; private set; }

        public MyTasksViewModel(UserPreferences userPreferences)
        {
            ProfileColor = userPreferences.ProfileColor;

            CreateScheduleCommand = new Command(async () => await CreateCleanSchedule());

            SeeFriendsTaskCommand = new Command(async () => await SeeFriendsTaskSelected());

            SeeDetailsMyTasksCommand = new Command(async () => await SeeMyTasksDetails());

            CompletedTodayTaskCommand = new Command(async (id) => await CompletedTaskTodaySelected(id.ToString()));
        }

        private async Task CompletedTaskTodaySelected(string id)
        {
            var task = Model.Tasks.First(c => c.Id.Equals(id));
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new ConfirmAction(string.Format(ResourceText.TITLE_ROOM_CLEANED, task.Room), ResourceText.DESCRIPTION_ROOM_CLEANED, View.Modal.Type.Green, new Command(async() => { await ClompletedTaskToday(id); })));
        }

        private async Task SeeFriendsTaskSelected()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<SeeScheduleAllFriendsViewModel>();
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private async Task SeeMyTasksDetails()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<DetailsUserScheduleViewModel>();
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    
        private async Task ClompletedTaskToday(string id)
        {

        }

        private async Task CreateCleanSchedule()
        {
            try
            {
                await ShowLoading();

                await Navigation.PushAsync<CreateScheduleViewModel>((viewModel, page) =>
                {
                    viewModel.CallbackOnCreateScheduleCommand = new Command(async () =>
                    {
                        ScheduleCreated = true;
                        await GetSchedule();

                        OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                        OnPropertyChanged(new PropertyChangedEventArgs("ScheduleCreated"));
                    });
                });

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private async Task GetSchedule()
        {
            Model = new MyTasksCleanHouseModel
            {
                Name = "Pablo Henrique",
                Month = DateTime.Today,
                Tasks = new System.Collections.ObjectModel.ObservableCollection<TasksForTheMonth>
                {
                    new TasksForTheMonth
                    {
                        Id = "1",
                        Room = "Área de Serviço",
                        CleaningRecords = 5,
                        LastRecord = DateTime.Today
                    },
                    new TasksForTheMonth
                    {
                        Id = "2",
                        Room = "Banheiro",
                        CleaningRecords = 0,
                        LastRecord = null
                    }
                }
            };
        }
    }
}
