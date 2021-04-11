using IntelligentHabitacion.App.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;

namespace IntelligentHabitacion.App.ViewModel.CleaningSchedule
{
    public class DetailsAllRateViewModel : BaseViewModel
    {
        public ObservableCollection<RateTaskModel> Model { get; set; }

        public DetailsAllRateViewModel()
        {
            CurrentState = LayoutState.Loading;
        }

        public async Task Initialize(string taskId)
        {
            

            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
    }
}
