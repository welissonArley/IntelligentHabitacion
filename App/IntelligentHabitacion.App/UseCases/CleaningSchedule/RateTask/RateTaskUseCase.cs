using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.CleaningSchedule;
using IntelligentHabitacion.Communication.Request;
using Refit;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.RateTask
{
    public class RateTaskUseCase : UseCaseBase, IRateTaskUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public RateTaskUseCase(Lazy<UserPreferences> userPreferences) : base("CleaningSchedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task<int> Execute(RateTaskModel model)
        {
            var token = await _userPreferences.GetToken();
            var response = await _restService.RateTask(model.TaskId, new RequestRateTaskJson
            {
                FeedBack = model.Feedback,
                Rating = model.RatingStars
            }, token, GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return response.Content.AverageRating;
        }
    }
}
