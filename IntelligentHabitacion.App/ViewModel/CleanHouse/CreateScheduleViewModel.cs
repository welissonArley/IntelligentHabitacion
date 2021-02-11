﻿using IntelligentHabitacion.App.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class CreateScheduleViewModel : BaseViewModel
    {
        public ManageScheduleModel Model { get; set; }
        public ICommand ManageTasksCommand { get; set; }
        public ICommand RandomAssignmentCommand { get; set; }
        public ICommand CallbackOnCreateScheduleCommand { get; set; }
        public ICommand ConcludeCommand { get; set; }

        public CreateScheduleViewModel()
        {
            ManageTasksCommand = new Command(async (userId) =>
            {
                await OnManageTasks(Model.UserTasks.First(c => c.Id.Equals(userId.ToString())));
            });

            ConcludeCommand = new Command(async (userId) =>
            {
                await OnConclude();
            });

            RandomAssignmentCommand = new Command(async () =>
            {
                await OnRandomAssignment();
            });

            Model = new ManageScheduleModel
            {
                RoomsAvaliables = new ObservableCollection<RoomModel>
                {
                    new RoomModel
                    {
                        Assigned = false,
                        Id = "5",
                        Room = "Sala de Jantar"
                    },
                    new RoomModel
                    {
                        Assigned = false,
                        Id = "6",
                        Room = "Corredor"
                    }
                },
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
                                Id = "2",
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
                                Id = "3",
                                Room = "Escritório"
                            },
                            new TasksForTheMonth
                            {
                                Id = "4",
                                Room = "Cozinha"
                            }
                        }
                    }
                }
            };
        }

        private async Task OnConclude()
        {
            CallbackOnCreateScheduleCommand.Execute(null);
            await Navigation.PopAsync();
        }

        private async Task OnRandomAssignment()
        {
            try
            {
                await ShowLoading();

                var newAvaliables = Model.UserTasks.SelectMany(c => c.Tasks).Select(c => new RoomModel
                {
                    Id = c.Id,
                    Room = c.Room,
                    Assigned = false
                }).ToList();

                newAvaliables.AddRange(Model.RoomsAvaliables);

                var userTasks = Model.UserTasks.Select(c => new AllFriendsGroup
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProfileColor = c.ProfileColor
                }).ToList();

                Model.UserTasks.Clear();

                var random = new Random();

                foreach (var user in userTasks)
                {
                    int index = random.Next(newAvaliables.Count);

                    user.Tasks = new ObservableCollection<TasksForTheMonth>
                    {
                        new TasksForTheMonth
                        {
                            Id = newAvaliables.ElementAt(index).Id,
                            Room = newAvaliables.ElementAt(index).Room
                        }
                    };

                    Model.UserTasks.Add(user);

                    newAvaliables.RemoveAt(index);
                }

                Model.RoomsAvaliables = new ObservableCollection<RoomModel>(newAvaliables);

                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private async Task OnManageTasks(AllFriendsGroup userGroup)
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<SelectTaskCleaningScheduleViewModel>((viewModel, page) =>
                {
                    var avaliables = userGroup.Tasks.Select(c => new RoomModel
                    {
                        Id = c.Id,
                        Assigned = true,
                        Room = c.Room
                    }).ToList();

                    avaliables.AddRange(Model.RoomsAvaliables);

                    viewModel.Model = userGroup;
                    viewModel.RoomsAvaliables = new ObservableCollection<RoomModel>(avaliables.OrderBy(c => c.Room));

                    viewModel.CallbackManageTasksCommand = new Command(async (RoomsAvaliables) =>
                    {
                        await ShowLoading();

                        var roomsAvaliables = RoomsAvaliables as ObservableCollection<RoomModel>;

                        Model.RoomsAvaliables = new ObservableCollection<RoomModel>(roomsAvaliables.Where(c => !c.Assigned));

                        var index = Model.UserTasks.IndexOf(userGroup);
                        Model.UserTasks.RemoveAt(index);

                        Model.UserTasks.Insert(index, new AllFriendsGroup
                        {
                            Id = userGroup.Id,
                            Name = userGroup.Name,
                            ProfileColor = userGroup.ProfileColor,
                            Tasks = new ObservableCollection<TasksForTheMonth>(roomsAvaliables.Where(c => c.Assigned).Select(c => new TasksForTheMonth
                            {
                                Id = c.Id,
                                Room = c.Room
                            }))
                        });

                        OnPropertyChanged(new PropertyChangedEventArgs("Model"));

                        HideLoading();
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
    }
}
