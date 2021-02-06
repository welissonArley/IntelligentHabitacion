using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class MyTasksViewModel : BaseViewModel
    {
        public string ProfileColor { get; set; }
        public MyTasksCleanHouseModel Model { get; set; }

        public ICommand SeeFriendsTaskCommand { get; private set; }
        public ICommand SeeDetailsMyTasksCommand { get; private set; }

        public MyTasksViewModel(UserPreferences userPreferences)
        {
            ProfileColor = userPreferences.ProfileColor;

            SeeFriendsTaskCommand = new Command(async () => await SeeFriendsTaskSelected());

            SeeDetailsMyTasksCommand = new Command(async () => await SeeMyTasksDetails());

            Model = new MyTasksCleanHouseModel
            {
                Name = "Pablo Henrique",
                Month = DateTime.Today,
                Tasks = new System.Collections.ObjectModel.ObservableCollection<TasksForTheMonth>
                {
                    new TasksForTheMonth
                    {
                        Room = "Área de Serviço",
                        CleaningRecords = 5,
                        LastRecord = DateTime.Today
                    },
                    new TasksForTheMonth
                    {
                        Room = "Banheiro",
                        CleaningRecords = 0,
                        LastRecord = null
                    }
                }
            };
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
    }
}
