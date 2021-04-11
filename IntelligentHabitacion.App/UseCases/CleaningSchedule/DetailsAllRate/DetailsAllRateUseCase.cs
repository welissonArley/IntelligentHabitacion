using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.CleaningSchedule;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.DetailsAllRate
{
    public class DetailsAllRateUseCase : UseCaseBase, IDetailsAllRateUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public DetailsAllRateUseCase(Lazy<UserPreferences> userPreferences) : base("CleaningSchedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task<IList<RateTaskModel>> Execute(string completedTaskId)
        {
            var token = await _userPreferences.GetToken();
            var response = await _restService.RateDetails(completedTaskId, token, GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content);
        }

        private List<RateTaskModel> Mapper(IList<ResponseRateTaskJson> response)
        {
            return response.Select(c => new RateTaskModel
            {
                Date = c.Date,
                Feedback = c.Feedback,
                Name = c.Name,
                RatingStars = c.Rating,
                Room = c.Room
            }).ToList();
        }
    }
}
