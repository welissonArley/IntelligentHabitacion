using IntelligentHabitacion.App.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class DetailsUserScheduleViewModel : BaseViewModel
    {
        public DetailsUserScheduleModel Model { get; set; }

        public ICommand SeeDetailsRatingCommand { get; private set; }
        public ICommand RatingFriendCommand { get; private set; }

        public DetailsUserScheduleViewModel()
        {
            Model = new DetailsUserScheduleModel
            {
                Name = "Welisson Arley",
                Month = DateTime.Today,
                ProfileColor = "#000000",
                Tasks = new ObservableCollection<TaskForTheMonthDetailsGroup>
                {
                    new TaskForTheMonthDetailsGroup(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 5), new ObservableCollection<TaskForTheMonthDetails>
                    {
                        new TaskForTheMonthDetails
                        {
                            Id = "1",
                            CanBeRate = true,
                            RatingStars = 4,
                            Room = "Área de Serviço"
                        }
                    }),
                    new TaskForTheMonthDetailsGroup(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), new ObservableCollection<TaskForTheMonthDetails>
                    {
                        new TaskForTheMonthDetails
                        {
                            Id = "2",
                            CanBeRate = false,
                            RatingStars = -1,
                            Room = "Área de Serviço"
                        },
                        new TaskForTheMonthDetails
                        {
                            Id = "2",
                            CanBeRate = false,
                            RatingStars = 3,
                            Room = "Banheiro"
                        }
                    })
                }
            };

            RatingFriendCommand = new Command(async () => { await RatingFriendTask(); });
            SeeDetailsRatingCommand = new Command(async () => { await SeeDatailsRatingTask(); });
        }

        private async Task RatingFriendTask()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<RatingCleaningViewModel>();
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
    }
}
