using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.View.Modal.MenuOptions;
using IntelligentHabitacion.Communication.Enums;
using IntelligentHabitacion.Communication.Response;
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
        private readonly ICleaningScheduleRule _rule;

        public bool ScheduleCreated { get; set; }
        public string InfoMessage { get; set; }
        public NeedActionEnum? Action { get; set; }

        public string Name { get; private set; }
        public string ProfileColor { get; set; }
        public MyTasksCleanHouseModel Model { get; set; }

        public ICommand MenuOptionsCommand { protected set; get; }
        public ICommand SeeFriendsTaskCommand { get; private set; }
        public ICommand SeeDetailsMyTasksCommand { get; private set; }
        public ICommand CompletedTodayTaskCommand { get; private set; }

        public MyTasksViewModel(UserPreferences userPreferences, ICleaningScheduleRule rule)
        {
            _rule = rule;

            Name = userPreferences.Name;
            ProfileColor = userPreferences.ProfileColor;

            SeeFriendsTaskCommand = new Command(async () => await SeeFriendsTaskSelected());

            SeeDetailsMyTasksCommand = new Command(async () => await SeeMyTasksDetails());

            CompletedTodayTaskCommand = new Command(async (id) => await CompletedTaskTodaySelected(id.ToString()));

            MenuOptionsCommand = new Command(async () =>
            {
                await ShowAdministratorOptions();
            });
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
            try
            {
                await ShowLoading();
                await _rule.TaskCompletedToday(id);

                var model = Model.Tasks.First(c => c.Id.Equals(id));
                var index = Model.Tasks.IndexOf(model);
                Model.Tasks.RemoveAt(index);

                Model.Tasks.Insert(index, new TasksForTheMonth
                {
                    Id = model.Id,
                    Room = model.Room,
                    CleaningRecords = model.CleaningRecords + 1,
                    LastRecord = DateTime.UtcNow
                });

                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                OnPropertyChanged(new PropertyChangedEventArgs("Model.Tasks"));

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private async Task ShowAdministratorOptions()
        {
            var navigation = Resolver.Resolve<INavigation>();

            if (Action == NeedActionEnum.RegisterRoom)
                await ShowQuickInformation(ResourceText.INFORMATION_YOU_CAN_NOT_PERFORM_THIS_ACTION);
            else
            {
                await navigation.PushPopupAsync(new AdministratorMyTasksModal(
                    new Command(async () =>
                    {
                        await CreateCleanSchedule();
                    }),
                    new Command(async () =>
                    {
                        await ShowConfigurationsSchedule();
                    }))
                );
            }
        }

        private async Task ShowConfigurationsSchedule()
        {
            try
            {
                await ShowLoading();

                await Navigation.PushAsync<SettingScheduleViewModel>();

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private async Task CreateCleanSchedule()
        {
            try
            {
                await ShowLoading();

                await Navigation.PushAsync<CreateScheduleViewModel>(async (viewModel, page) =>
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
            var response = await _rule.GetMyTasks();
            var action = (ResponseMyTasksCleaningScheduleJson)response;
            Model = new MyTasksCleanHouseModel
            {
                Name = Name,
                Month = System.DateTime.UtcNow,
                Tasks = new System.Collections.ObjectModel.ObservableCollection<TasksForTheMonth>(action.Tasks.Select(c => new TasksForTheMonth
                {
                    Id = c.Id,
                    Room = c.Room,
                    CleaningRecords = c.CleaningRecords,
                    LastRecord = c.LastRecord
                }))
            };
        }
    }
}
