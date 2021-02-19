using IntelligentHabitacion.App.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class SelectTaskCleaningScheduleViewModel : BaseViewModel
    {
        public AllFriendsGroup Model { get; set; }
        public ObservableCollection<RoomScheduleModel> RoomsAvaliables { get; set; }
        public ICommand ConcludeCommand { get; set; }
        public ICommand CallbackManageTasksCommand { get; set; }

        public SelectTaskCleaningScheduleViewModel()
        {
            ConcludeCommand = new Command(async () =>
            {
                await Navigation.PopAsync();
                CallbackManageTasksCommand?.Execute(RoomsAvaliables);
            });
        }
    }
}
