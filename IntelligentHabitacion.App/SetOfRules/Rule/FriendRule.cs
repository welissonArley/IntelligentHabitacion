using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class FriendRule : IFriendRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;
        private readonly UserPreferences _userPreferences;

        public FriendRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, UserPreferences userPreferences)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _userPreferences = userPreferences;
        }

        public async Task ChangeAdministrator(string code, string friendId, string password)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new CodeEmptyException();

            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();

            var response = await _httpClient.ChangeAdministrator(friendId, new RequestAdminActionJson
            {
                Code = code,
                Password = password
            }, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.UserIsAdministrator(false);
            _userPreferences.ChangeToken(response.Token);
        }

        public async Task RemoveFriend(string code, string friendId, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();

            if (string.IsNullOrWhiteSpace(code))
                throw new CodeEmptyException();

            var response = await _httpClient.RemoveFriend(friendId, new RequestAdminActionJson
            {
                Code = code,
                Password = password
            }, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task<FriendModel> ChangeDateJoinOn(string friendId, DateTime date)
        {
            var response = await _httpClient.ChangeDateJoinHome(friendId, new RequestChangeDateJoinHomeJson
            {
                JoinOn = date
            }, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);

            var responseFriend = (ResponseFriendJson)response.Response;

            return new FriendModel
            {
                Id = responseFriend.Id,
                Name = responseFriend.Name,
                ProfileColor = responseFriend.ProfileColor,
                JoinedOn = responseFriend.JoinedOn,
                DescriptionDateJoined = responseFriend.DescriptionDateJoined,
                Phonenumber1 = responseFriend.Phonenumbers[0].Number,
                Phonenumber2 = responseFriend.Phonenumbers.Count > 1 ? responseFriend.Phonenumbers[1].Number : null,
                EmergencyContact1 = new EmergencyContactModel
                {
                    Name = responseFriend.EmergencyContacts[0].Name,
                    Relationship = responseFriend.EmergencyContacts[0].Relationship,
                    PhoneNumber = responseFriend.EmergencyContacts[0].Phonenumber
                },
                EmergencyContact2 = responseFriend.EmergencyContacts.Count > 1 ? new EmergencyContactModel
                {
                    Name = responseFriend.EmergencyContacts[1].Name,
                    Relationship = responseFriend.EmergencyContacts[1].Relationship,
                    PhoneNumber = responseFriend.EmergencyContacts[1].Phonenumber
                } : null
            };
        }

        public async Task<List<FriendModel>> GetHouseFriends()
        {
            var response = await _httpClient.GetHouseFriends(_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);

            var responseFriends = (List<ResponseFriendJson>)response.Response;

            return responseFriends.Select(c => new FriendModel
            {
                Id = c.Id,
                Name = c.Name,
                ProfileColor = c.ProfileColor,
                JoinedOn = c.JoinedOn,
                DescriptionDateJoined = c.DescriptionDateJoined,
                Phonenumber1 = c.Phonenumbers[0].Number,
                Phonenumber2 = c.Phonenumbers.Count > 1 ? c.Phonenumbers[1].Number : null,
                EmergencyContact1 = new EmergencyContactModel
                {
                    Name = c.EmergencyContacts[0].Name,
                    Relationship = c.EmergencyContacts[0].Relationship,
                    PhoneNumber = c.EmergencyContacts[0].Phonenumber
                },
                EmergencyContact2 = c.EmergencyContacts.Count > 1 ? new EmergencyContactModel
                {
                    Name = c.EmergencyContacts[1].Name,
                    Relationship = c.EmergencyContacts[1].Relationship,
                    PhoneNumber = c.EmergencyContacts[1].Phonenumber
                } : null
            }).ToList();
        }

        public async Task NotifyFriendOrderHasArrived(string friendId)
        {
            var response = await _httpClient.NotifyFriendOrderHasArrived(friendId, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task RequestCodeToChangeAdministrator()
        {
            var response = await _httpClient.RequestCodeToChangeAdministrator(_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task RequestCodeToRemoveFriend()
        {
            var response = await _httpClient.RequestCodeToRemoveFriend(_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }
    }
}
