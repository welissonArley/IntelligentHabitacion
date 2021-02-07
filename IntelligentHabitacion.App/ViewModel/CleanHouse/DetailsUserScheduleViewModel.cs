using IntelligentHabitacion.App.Model;
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
                            Id = "3",
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
                await Navigation.PushAsync<RatingCleaningViewModel>((viewModel, page) =>
                {
                    viewModel.Model = new RatingCleaningModel
                    {
                        Id = "1",
                        Date = DateTime.Today,
                        Name = "Welisson Arley",
                        Room = "Banheiro"
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
    }
}
