using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
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

        public ICleaningScheduleRule _rule { get; private set; }

        public CreateScheduleViewModel(ICleaningScheduleRule rule)
        {
            _rule = rule;

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

            Model = Task.Run(async() => await _rule.GetSchedule()).Result;
        }

        private async Task OnConclude()
        {
            try
            {
                await ShowLoading();

                await _rule.UpdateSchedule(Model);

                CallbackOnCreateScheduleCommand.Execute(null);
                await Navigation.PopAsync();

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private async Task OnRandomAssignment()
        {
            try
            {
                await ShowLoading();

                var newAvaliables = Model.UserTasks.SelectMany(c => c.Tasks).Select(c => new RoomScheduleModel
                {
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
                            Room = newAvaliables.ElementAt(index).Room
                        }
                    };

                    Model.UserTasks.Add(user);

                    newAvaliables.RemoveAt(index);
                }

                Model.RoomsAvaliables = new ObservableCollection<RoomScheduleModel>(newAvaliables);

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
                    var avaliables = userGroup.Tasks.Select(c => new RoomScheduleModel
                    {
                        Assigned = true,
                        Room = c.Room
                    }).ToList();

                    avaliables.AddRange(Model.RoomsAvaliables);

                    viewModel.Model = userGroup;
                    viewModel.RoomsAvaliables = new ObservableCollection<RoomScheduleModel>(avaliables.OrderBy(c => c.Room));

                    viewModel.CallbackManageTasksCommand = new Command(async (RoomsAvaliables) =>
                    {
                        await ShowLoading();

                        var roomsAvaliables = RoomsAvaliables as ObservableCollection<RoomScheduleModel>;

                        Model.RoomsAvaliables = new ObservableCollection<RoomScheduleModel>(roomsAvaliables.Where(c => !c.Assigned));

                        var index = Model.UserTasks.IndexOf(userGroup);
                        Model.UserTasks.RemoveAt(index);

                        Model.UserTasks.Insert(index, new AllFriendsGroup
                        {
                            Id = userGroup.Id,
                            Name = userGroup.Name,
                            ProfileColor = userGroup.ProfileColor,
                            Tasks = new ObservableCollection<TasksForTheMonth>(roomsAvaliables.Where(c => c.Assigned).Select(c => new TasksForTheMonth
                            {
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
