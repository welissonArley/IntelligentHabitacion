using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class CleaningScheduleRule : ICleaningScheduleRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;
        private readonly UserPreferences _userPreferences;

        public CleaningScheduleRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, UserPreferences userPreferences)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _userPreferences = userPreferences;
        }

        public async Task<DetailsUserScheduleModel> GetDetailsAllTasksUserForAMonth(string id, DateTime date)
        {
            var response = await _httpClient.GetUsersTaskDetails(_userPreferences.Token, id, new RequestDateJson { Date = date }, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);

            var json = (ResponseDetailsUserScheduleJson)response.Response;

            var group = json.Tasks.GroupBy(c => c.Date.Date);

            return new DetailsUserScheduleModel
            {
                Month = json.Month,
                Name = json.Name,
                ProfileColor = json.ProfileColor,
                Tasks = new ObservableCollection<TaskForTheMonthDetailsGroup>(
                    group.Select(c => new TaskForTheMonthDetailsGroup(c.Key, c.Select(k => new TaskForTheMonthDetails
                    {
                        CanBeRate = k.CanBeRate,
                        Id = k.Id,
                        RatingStars = k.AverageRating,
                        Room = k.Room
                    }).ToList())))
            };
        }

        public async Task<object> GetMyTasks(DateTime? date = null)
        {
            var response = await _httpClient.GetMyTasksCleaningSchedule(_userPreferences.Token, new RequestDateJson { Date = date ?? DateTime.UtcNow }, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);

            return response.Response;
        }

        public async Task<ManageScheduleModel> GetSchedule()
        {
            var response = await _httpClient.GetCleaningSchedule(_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);

            var json = (ResponseManageScheduleJson)response.Response;

            return new ManageScheduleModel
            {
                RoomsAvaliables = new ObservableCollection<RoomScheduleModel>(json.RoomsAvaliables.Select(c => new RoomScheduleModel
                {
                    Assigned = false, Room = c.Name
                })),
                UserTasks = new ObservableCollection<AllFriendsGroup>(json.UserTasks.Select(c => new AllFriendsGroup
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProfileColor = c.ProfileColor,
                    Tasks = new ObservableCollection<TasksForTheMonth>(c.Tasks.Select(k => new TasksForTheMonth
                    {
                        Id = c.Id,
                        Room = k.Room,
                        CleaningRecords = k.CleaningRecords,
                        LastRecord = k.LastRecord
                    }))
                }))
            };
        }

        public async Task TaskCompletedToday(string id)
        {
            var response = await _httpClient.TaskCompletedToday(_userPreferences.Token, id, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task UpdateSchedule(ManageScheduleModel model)
        {
            var json = model.UserTasks.Select(c => new RequestUpdateCleaningScheduleJson
            {
                UserId = c.Id,
                Rooms = c.Tasks.Select(k => k.Room).ToList()
            }).ToList();

            var response = await _httpClient.UpdateCleaningSchedule(_userPreferences.Token, json, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task<AllFriendsTasksModel> GetFriendsTasks(DateTime date)
        {
            var response = await _httpClient.GetFriendsTasks(_userPreferences.Token, new RequestDateJson { Date = date }, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);

            var json = (List<ResponseAllFriendsTasksScheduleJson>)response.Response;

            return new AllFriendsTasksModel
            {
                Month = date,
                FriendsTasks = new ObservableCollection<AllFriendsGroup>(json.Select(c => new AllFriendsGroup
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProfileColor = c.ProfileColor,
                    Tasks = new ObservableCollection<TasksForTheMonth>(c.Tasks.Select(k => new TasksForTheMonth
                    {
                        Id = k.Id,
                        Room = k.Room,
                        CleaningRecords = k.CleaningRecords
                    }))
                }))
            };
        }
    }
}
