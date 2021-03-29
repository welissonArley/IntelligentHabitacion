using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.CleaningSchedule;
using IntelligentHabitacion.Communication.Request;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.CreateFirstSchedule
{
    public class CreateFirstScheduleUseCase : UseCaseBase, ICreateFirstScheduleUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public CreateFirstScheduleUseCase(Lazy<UserPreferences> userPreferences) : base("CleaningSchedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task Execute(IList<FriendCreateFirstScheduleModel> usersTasks)
        {
            var request = Mapper(usersTasks);

            var response = await _restService.CreateFirstCleaningSchedule(request, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
        }

        private IList<RequestUpdateCleaningScheduleJson> Mapper(IList<FriendCreateFirstScheduleModel> usersTasks)
        {
            return usersTasks.Select(c => new RequestUpdateCleaningScheduleJson
            {
                UserId = c.Id,
                Rooms = c.Tasks.Select(w => w.Room).ToList()
            }).ToList();
        }
    }
}
