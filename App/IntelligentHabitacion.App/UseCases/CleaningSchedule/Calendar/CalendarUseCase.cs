using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.CleaningSchedule;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.Calendar
{
    public class CalendarUseCase : UseCaseBase, ICalendarUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public CalendarUseCase(Lazy<UserPreferences> userPreferences) : base("CleaningSchedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task<CleaningScheduleCalendarModel> Execute(DateTime month, string room = null)
        {
            var token = await _userPreferences.GetToken();
            var response = await _restService.Calendar(new RequestCalendarCleaningScheduleJson
            {
                Month = month,
                Room = room
            }, token, GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content);
        }

        private CleaningScheduleCalendarModel Mapper(ResponseCalendarCleaningScheduleJson response)
        {
            return new CleaningScheduleCalendarModel
            {
                Date = response.Date,
                CleanedDays = response.CleanedDays.Select(c => new CleaningScheduleCalendarDayInfoModel
                {
                    Day = c.Day,
                    AmountCleanedRecords = c.AmountCleanedRecords,
                    AmountcleanedRecordsToRate = c.AmountcleanedRecordsToRate
                }).ToList()
            };
        }
    }
}
