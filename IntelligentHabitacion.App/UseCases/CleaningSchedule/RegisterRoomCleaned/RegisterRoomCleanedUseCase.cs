using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.CleaningSchedule;
using IntelligentHabitacion.Communication.Request;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.RegisterRoomCleaned
{
    public class RegisterRoomCleanedUseCase : UseCaseBase, IRegisterRoomCleanedUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public RegisterRoomCleanedUseCase(Lazy<UserPreferences> userPreferences) : base("CleaningSchedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task Execute(IList<string> taskIds, DateTime date)
        {
            var token = await _userPreferences.GetToken();
            var response = await _restService.RegisterRoomCleaned(new RequestRegisterRoomCleaned
            {
                Date = date,
                TaskIds = taskIds
            }, token, GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
        }
    }
}
