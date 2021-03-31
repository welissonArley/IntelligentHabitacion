﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.CleaningSchedule;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.GetTasks
{
    public class GetTasksUseCase : UseCaseBase, IGetTasksUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public GetTasksUseCase(Lazy<UserPreferences> userPreferences) : base("CleaningSchedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task<ScheduleCleaningHouseModel> Execute(DateTime date)
        {
            var token = await _userPreferences.GetToken();
            var response = await _restService.GetTasks(new RequestDateJson { Date = date }, token, GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content);
        }

        private ScheduleCleaningHouseModel Mapper(ResponseTasksJson response)
        {
            return new ScheduleCleaningHouseModel
            {
                Action = response.Action,
                Message = response.Message,
                Schedule = new ScheduleTasksCleaningHouseModel
                {
                    Date = response.Schedule.Date,
                    AmountOfTasks = response.Schedule.AmountOfTasks,
                    Name = response.Schedule.Name,
                    ProfileColor = response.Schedule.ProfileColor,
                    Tasks = new ObservableCollection<TaskModel>(response.Schedule.Tasks.Select(c => new TaskModel
                    {
                        IdTaskToRegisterRoomCleaning = c.IdTaskToRegisterRoomCleaning,
                        CanRegisterRoomCleanedToday = c.CanCompletedToday,
                        CanEdit = c.CanEdit,
                        CanRate = c.CanRate,
                        Room = c.Room,
                        Assign = new ObservableCollection<UserSimplifiedModel>(c.Assign.Select(w => new UserSimplifiedModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            ProfileColor = w.ProfileColor
                        }))
                    }))
                },
                CreateSchedule = new CreateScheduleCleaningHouseModel
                {
                    Rooms = response.CreateSchedule.Rooms.Select(c => new RoomModel
                    {
                        Id = c.Id,
                        Room = c.Name
                    }).ToList(),
                    Friends = new ObservableCollection<FriendCreateFirstScheduleModel>(response.CreateSchedule.Friends.Select(c => new FriendCreateFirstScheduleModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ProfileColor = c.ProfileColor
                    }))
                }
            };
        }
    }
}
