﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.Friend;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Friends.GetMyFriends
{
    public class GetMyFriendsUseCase : UseCaseBase, IGetMyFriendsUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IFriendService _restService;

        public GetMyFriendsUseCase(Lazy<UserPreferences> userPreferences) : base("Friend")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IFriendService>(BaseAddress());
        }

        public async Task<IList<FriendModel>> Execute()
        {
            var response = await _restService.GetHouseFriends(await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content);
        }

        private IList<FriendModel> Mapper(List<ResponseFriendJson> myFriendsJsons)
        {
            return myFriendsJsons.Select(c => new FriendModel
            {
                Id = c.Id,
                Name = c.Name,
                ProfileColor = c.ProfileColor,
                JoinedOn = c.JoinedOn,
                DescriptionDateJoined = c.DescriptionDateJoined,
                Phonenumbers = c.Phonenumbers.Select(w => w.Number).ToList(),
                EmergencyContacts = c.EmergencyContacts.Select(w => new EmergencyContactModel
                {
                    Name = w.Name,
                    Relationship = w.Relationship,
                    PhoneNumber = w.Phonenumber
                }).ToList()
            }).ToList();
        }
    }
}
