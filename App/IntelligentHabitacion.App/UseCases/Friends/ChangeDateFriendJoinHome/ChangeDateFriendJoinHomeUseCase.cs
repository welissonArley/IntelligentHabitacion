using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.Friend;
using IntelligentHabitacion.Communication.Request;
using Refit;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Friends.ChangeDateFriendJoinHome
{
    public class ChangeDateFriendJoinHomeUseCase : UseCaseBase, IChangeDateFriendJoinHomeUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IFriendService _restService;

        public ChangeDateFriendJoinHomeUseCase(Lazy<UserPreferences> userPreferences) : base("Friend")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IFriendService>(BaseAddress());
        }

        public async Task<FriendModel> Execute(string friendId, DateTime date)
        {
            var response = await _restService.ChangeDateJoinHome(friendId, new RequestDateJson { Date = date } , await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return new FriendModel { DescriptionDateJoined = response.Content.DescriptionDateJoined, JoinedOn = response.Content.JoinedOn};
        }
    }
}
