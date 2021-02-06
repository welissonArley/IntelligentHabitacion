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
    public class SeeScheduleAllFriendsViewModel : BaseViewModel
    {
        public ICommand SearchTextChangedCommand { protected set; get; }
        public ICommand TappedSeeDetailsCommand { protected set; get; }

        public AllFriendsTasksModel Model { get; set; }

        private ObservableCollection<AllFriendsGroup> _friendsTask { get; set; }

        public SeeScheduleAllFriendsViewModel()
        {
            SearchTextChangedCommand = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });

            TappedSeeDetailsCommand = new Command(async (value) =>
            {
                await SeeFriendTasksDetails((string)value);
            });

            Model = new AllFriendsTasksModel
            {
                Month = DateTime.Today,
                FriendsTasks = new ObservableCollection<AllFriendsGroup>
                {
                    new AllFriendsGroup
                    {
                        Name = "Matheus Gomes",
                        ProfileColor = "#65BCBF",
                        CanAssignTasks = true,
                        Tasks = new ObservableCollection<TasksForTheMonth>()
                    },
                    new AllFriendsGroup
                    {
                        Name = "William Rodrigues",
                        ProfileColor = "#65BCBF",
                        CanAssignTasks = false,
                        Tasks = new ObservableCollection<TasksForTheMonth>
                        {
                            new TasksForTheMonth
                            {
                                CleaningRecords = 5,
                                Room = "Área de Serviço"
                            }
                        }
                    },
                    new AllFriendsGroup
                    {
                        Name = "Anilton Barbosa",
                        ProfileColor = "#657EBF",
                        CanAssignTasks = false,
                        Tasks = new ObservableCollection<TasksForTheMonth>
                        {
                            new TasksForTheMonth
                            {
                                CleaningRecords = 4,
                                Room = "Banheiro"
                            },
                            new TasksForTheMonth
                            {
                                CleaningRecords = 2,
                                Room = "Sala"
                            }
                        }
                    },
                    new AllFriendsGroup
                    {
                        Name = "Pablo Henrique",
                        ProfileColor = "#BF658B",
                        CanAssignTasks = false,
                        Tasks = new ObservableCollection<TasksForTheMonth>
                        {
                            new TasksForTheMonth
                            {
                                CleaningRecords = 3,
                                Room = "Cozinha"
                            }
                        }
                    }
                }
            };

            _friendsTask = Model.FriendsTasks;
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
