using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using System;
using System.Collections.ObjectModel;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class MyTasksViewModel : BaseViewModel
    {
        public MyTasksModel Model { get; set; }

        public MyTasksViewModel(UserPreferences userPreferences)
        {
            Model = new MyTasksModel
            {
                Name = "Pablo Henrique",
                Month = DateTime.Today,
                ProfileColor = userPreferences.ProfileColor,
                MyTasksForTheMonth = new ObservableCollection<MyTask>
                {
                    new MyTask
                    {
                        Room = "Área de Serviço"
                    }
                },
                Historics = new ObservableCollection<Historic>
                {
                    new Historic(DateTime.Today, new ObservableCollection<MyTaskHistoric>
                    {
                        new MyTaskHistoric
                        {
                            RatingStars = 5,
                            Room = "Banheiro"
                        },
                        new MyTaskHistoric
                        {
                            RatingStars = 4,
                            Room = "Cozinha"
                        }
                    })
                }
            };
        }
    }
}
