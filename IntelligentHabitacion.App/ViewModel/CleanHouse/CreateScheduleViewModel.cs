using IntelligentHabitacion.App.Model;
using System.Collections.ObjectModel;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class CreateScheduleViewModel : BaseViewModel
    {
        public ManageScheduleModel Model { get; set; }

        public CreateScheduleViewModel()
        {
            Model = new ManageScheduleModel
            {
                UserTasks = new ObservableCollection<AllFriendsGroup>
                {
                    new AllFriendsGroup
                    {
                        Id = "1",
                        Name = "Matheus Gomes",
                        ProfileColor = "#000000",
                        Tasks = new ObservableCollection<TasksForTheMonth>()
                    },
                    new AllFriendsGroup
                    {
                        Id = "2",
                        Name = "William Rodrigues",
                        ProfileColor = "#65BCBF",
                        Tasks = new ObservableCollection<TasksForTheMonth>
                        {
                            new TasksForTheMonth
                            {
                                Id = "1",
                                Room = "Área de serviço"
                            }
                        }
                    },
                    new AllFriendsGroup
                    {
                        Id = "3",
                        Name = "Anilton Barbosa",
                        ProfileColor = "#657EBF",
                        Tasks = new ObservableCollection<TasksForTheMonth>
                        {
                            new TasksForTheMonth
                            {
                                Id = "1",
                                Room = "Banheiro"
                            }
                        }
                    },
                    new AllFriendsGroup
                    {
                        Id = "4",
                        Name = "Pablo Henrique",
                        ProfileColor = "#BF658B",
                        Tasks = new ObservableCollection<TasksForTheMonth>
                        {
                            new TasksForTheMonth
                            {
                                Id = "1",
                                Room = "Escritório"
                            },
                            new TasksForTheMonth
                            {
                                Id = "1",
                                Room = "Cozinha"
                            }
                        }
                    }
                }
            };
        }
    }
}
