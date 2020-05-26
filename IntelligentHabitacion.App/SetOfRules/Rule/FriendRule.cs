using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class FriendRule : IFriendRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;
        private readonly ISqliteDatabase _database;

        public FriendRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, ISqliteDatabase database)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _database = database;
        }

        public async Task<List<FriendModel>> GetHouseFriends()
        {
            var response = await _httpClient.GetHouseFriends(_database.Get().Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _database.UpdateToken(response.Token);

            var responseFriends = (List<ResponseFriendJson>)response.Response;

            return responseFriends.Select(c => new FriendModel
            {
                Id = c.Id,
                Name = c.Name,
                ProfileColor = c.ProfileColor,
                JoinedOn = c.JoinedOn,
                Phonenumber1 = c.Phonenumbers[0].Number,
                Phonenumber2 = c.Phonenumbers.Count > 1 ? c.Phonenumbers[1].Number : null,
                EmergencyContact1 = new EmergencyContactModel
                {
                    Name = c.EmergencyContact[0].Name,
                    Relationship = c.EmergencyContact[0].Relationship,
                    PhoneNumber = c.EmergencyContact[0].Phonenumber
                },
                EmergencyContact2 = c.EmergencyContact.Count > 1 ? new EmergencyContactModel
                {
                    Name = c.EmergencyContact[1].Name,
                    Relationship = c.EmergencyContact[1].Relationship,
                    PhoneNumber = c.EmergencyContact[1].Phonenumber
                } : null
            }).ToList();
        }
    }
}
