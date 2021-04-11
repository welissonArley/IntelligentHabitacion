﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.CleaningSchedule;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.HistoryOfTheDay
{
    public class HistoryOfTheDayUseCase : UseCaseBase, IHistoryOfTheDayUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public HistoryOfTheDayUseCase(Lazy<UserPreferences> userPreferences) : base("CleaningSchedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task<IList<DetailsTaskCleanedOnDayModelGroup>> Execute(DateTime date, string room = null)
        {
            var token = await _userPreferences.GetToken();
            var response = await _restService.HistoryOfTheDay(new RequestHistoryOfTheDayJson
            {
                Date = date,
                Room = room
            }, token, GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content);
        }

        private IList<DetailsTaskCleanedOnDayModelGroup> Mapper(IList<ResponseHistoryRoomOfTheDayJson> response)
        {
            return response.Select(c => new DetailsTaskCleanedOnDayModelGroup(c.Room, c.History.Select(w => new DetailsTaskCleanedOnDayModel
            {
                AverageRate = w.AverageRate,
                CanRate = w.CanRate,
                Id = w.Id,
                User = w.User,
                CleanedAt = w.CleanedAt
            }).ToList())).ToList();
        }
    }
}
