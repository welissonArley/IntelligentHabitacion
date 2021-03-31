using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.CreateFirstSchedule;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.GetTasks;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.RegisterRoomCleaned;
using IntelligentHabitacion.App.UseCases.CleaningSchedule.Reminder;
using IntelligentHabitacion.App.View.Modal.MenuOptions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.CleaningSchedule
{
    public class TasksViewModel : BaseViewModel
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private readonly Lazy<ICreateFirstScheduleUseCase> createFirstScheduleUseCase;
        private readonly Lazy<IGetTasksUseCase> getTasksUseCase;
        private readonly Lazy<IRegisterRoomCleanedUseCase> registerRoomCleanedUseCase;
        private readonly Lazy<IReminderUseCase> reminderUseCase;
        private UserPreferences _userPreferences => userPreferences.Value;
        private IGetTasksUseCase _getTasksUseCase => getTasksUseCase.Value;
        private ICreateFirstScheduleUseCase _createFirstScheduleUseCase => createFirstScheduleUseCase.Value;
        private IRegisterRoomCleanedUseCase _registerRoomCleanedUseCase => registerRoomCleanedUseCase.Value;
        private IReminderUseCase _reminderUseCase => reminderUseCase.Value;

        public ScheduleCleaningHouseModel Model { get; set; }

        public ICommand RegisterRoomClenedTodayCommand { get; }
        public ICommand ConcludeCreateFirstScheduleCommand { get; }
        public ICommand RandomAssignmentCommand { get; }
        public ICommand ManageTasksCommand { get; }
        public ICommand OnDateSelectedCommand { get; }
        public ICommand FloatActionCommand { get; }

        public TasksViewModel(Lazy<IGetTasksUseCase> getTasksUseCase,
            Lazy<ICreateFirstScheduleUseCase> createFirstScheduleUseCase,
            Lazy<IRegisterRoomCleanedUseCase> registerRoomCleanedUseCase,
            Lazy<IReminderUseCase> reminderUseCase, Lazy<UserPreferences> userPreferences)
        {
            CurrentState = LayoutState.Loading;
            
            this.getTasksUseCase = getTasksUseCase;
            this.createFirstScheduleUseCase = createFirstScheduleUseCase;
            this.registerRoomCleanedUseCase = registerRoomCleanedUseCase;
            this.reminderUseCase = reminderUseCase;
            this.userPreferences = userPreferences;

            RandomAssignmentCommand = new Command(OnRandomAssignment);
            ManageTasksCommand = new Command(OnManageTasksCommand);
            RegisterRoomClenedTodayCommand = new Command(async(room) => await OnRegisterRoomClenedToday((TaskModel)room));
            ConcludeCreateFirstScheduleCommand = new Command(async() =>
            {
                await OnConcludeCreateFirstSchedule();
            });
            OnDateSelectedCommand = new Command(async (date) =>
            {
                await OnDateSelected((DateTime)date);
            });

            ICommand SelectRegisterRoomsCleanedCommand = new Command(async() =>
            {
                await Navigation.PushAsync<SelectRoomsRegisterCleanedViewModel>((viewModel, _) =>
                {
                    var tasks = Model.Schedule.Tasks.Where(c => !string.IsNullOrEmpty(c.IdTaskToRegisterRoomCleaning)).ToList();
                    viewModel.Initialize(tasks, new Command(async(listAssigns) =>
                    {
                        await OnCallbackRegisterCleanedRoomsToday((List<string>)listAssigns);
                    }));
                });
            });
            ICommand SelectRememberUserCleanRoom = new Command(async () =>
            {
                await Navigation.PushAsync<SelectOptionsCleaningHouseViewModel>(async (viewModel, _) =>
                {
                    var options = new List<SelectOptionModel>();
                    var myId = await _userPreferences.GetMyId();
                    var users = Model.Schedule.Tasks.SelectMany(c => c.Assign).Where(c => !c.Id.Equals(myId));
                    foreach(var id in users.Select(c => c.Id).Distinct())
                    {
                        var model = users.First(c => c.Id.Equals(id));
                        options.Add(new SelectOptionModel
                        {
                            Id = model.Id,
                            Name = model.Name,
                            Assigned = false
                        });
                    }

                    viewModel.Initialize(ResourceText.TITLE_REMINDER_PERFORM_TASK,
                        ResourceText.DESCRIPTION_REMINDER_PERFORM_TASK, ResourceText.TITLE_CHOOSE_WHO_WILL_RECEIVE_REMINDER_TWO_POINTS,
                        new Command(async (listAssigns) =>
                        {
                            var list = (List<SelectOptionModel>)listAssigns;
                            await OnSendReminderCleanRoom(list.Select(c => c.Id).ToList());
                        }), options);
                });
            });
            FloatActionCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new FloatActionTaskCleaningScheduleModal(SelectRegisterRoomsCleanedCommand, SelectRememberUserCleanRoom));
            });
        }

        private async Task OnDateSelected(DateTime date)
        {
            try
            {
                SendingData();

                Model = await _getTasksUseCase.Execute(date);

                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                CurrentState = LayoutState.None;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private void OnRandomAssignment()
        {
            CurrentState = LayoutState.Custom;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));

            var newAvaliables = Model.CreateSchedule.Rooms.Select(c => new RoomModel
            {
                Room = c.Room,
                Id = c.Id
            }).ToList();

            var random = new Random();

            foreach (var user in Model.CreateSchedule.Friends)
            {
                if (newAvaliables.Count == 0)
                    break;

                int index = random.Next(newAvaliables.Count);
                var roomRandom = newAvaliables.ElementAt(index);

                user.Tasks = new System.Collections.ObjectModel.ObservableCollection<RoomModel>
                {
                    new RoomModel
                    {
                        Id = roomRandom.Id,
                        Room = roomRandom.Room
                    }
                };

                newAvaliables.RemoveAt(index);
            }

            CurrentState = LayoutState.Empty;
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
        private void OnManageTasksCommand(object user)
        {
            var userModel = user as FriendCreateFirstScheduleModel;

            var listAssigned = userModel.Tasks.Select(c => new SelectOptionModel
            {
                Id = c.Id,
                Name = c.Room,
                Assigned = true
            }).ToList();

            var listAvaliable = Model.CreateSchedule.Rooms.Where(c => !listAssigned.Any(w => w.Name.Equals(c.Room)))
                .Select(c => new SelectOptionModel
            {
                Id = c.Id,
                Name = c.Room,
                Assigned = false
            });

            listAssigned.AddRange(listAvaliable);

            Navigation.PushAsync<SelectOptionsCleaningHouseViewModel>((viewModel, _) =>
            {
                viewModel.Initialize(userModel.Name, ResourceText.TITLE_SELECT_ROOMS_BELOW_TO_BE_CLEANED,
                    ResourceText.TITLE_CHOOSE_THE_ROOMS_TWO_POINTS, new Command((roomsAssignedsReturn) =>
                    {
                        CurrentState = LayoutState.Custom;
                        OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));

                        var roomsAssigneds = roomsAssignedsReturn as List<SelectOptionModel>;

                        userModel.Tasks = new System.Collections.ObjectModel.ObservableCollection<RoomModel>(
                            roomsAssigneds.Select(c => new RoomModel
                            {
                                Id = c.Id,
                                Room = c.Name
                            }));
                        
                        CurrentState = LayoutState.Empty;
                        OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                        OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
                    }), listAssigned.OrderBy(c => c.Name).ToList());
            });
        }
        private async Task OnConcludeCreateFirstSchedule()
        {
            try
            {
                SendingData();

                Model.Schedule = await _createFirstScheduleUseCase.Execute(Model.CreateSchedule.Friends);

                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                await Sucess();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
                CurrentState = LayoutState.Empty;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
            }
        }
        private async Task OnRegisterRoomClenedToday(TaskModel task)
        {
            try
            {
                SendingData();

                await _registerRoomCleanedUseCase.Execute(new List<string> { task.IdTaskToRegisterRoomCleaning }, DateTime.UtcNow);
                Model.Schedule.Tasks
                    .First(c => c.IdTaskToRegisterRoomCleaning.Equals(task.IdTaskToRegisterRoomCleaning)).CanRegisterRoomCleanedToday = false;

                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                await Sucess();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnCallbackRegisterCleanedRoomsToday(List<string> assigns)
        {
            foreach (var id in assigns)
                Model.Schedule.Tasks.First(c => c.IdTaskToRegisterRoomCleaning.Equals(id)).CanRegisterRoomCleanedToday = false;

            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            await Sucess();
        }
        private async Task OnSendReminderCleanRoom(List<string> assigns)
        {
            try
            {
                SendingData();

                await _reminderUseCase.Execute(assigns);

                await Sucess();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        public async Task Initialize()
        {
            try
            {
                Model = await _getTasksUseCase.Execute(DateTime.UtcNow);
                if (Model.Action == Communication.Enums.NeedAction.RegisterRoom || Model.Action == Communication.Enums.NeedAction.InformationCreateCleaningSchedule)
                {
                    CurrentState = LayoutState.Error;
                    OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
                }
                else if (Model.Action == Communication.Enums.NeedAction.CreateTheCleaningSchedule)
                {
                    CurrentState = LayoutState.Empty;
                    OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
                }
                else
                {
                    CurrentState = LayoutState.None;
                    OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
                }
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
