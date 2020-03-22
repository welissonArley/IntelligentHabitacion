using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Useful;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Mapper
{
    public class Mapper
    {
        #region MapperJsonToModel
        public User MapperJsonToModel(RequestRegisterUserJson userJson)
        {
            return new User
            {
                Active = true,
                CreateDate = DateTimeController.DateTimeNow(),
                Email = userJson.Email,
                Name = userJson.Name,
                Password = userJson.Password,
                EmergecyContacts = userJson.EmergencyContacts.Select(c => MapperJsonToModel(c)).ToList(),
                Phonenumbers = userJson.Phonenumbers.Select(c => MapperJsonToModel(c)).ToList()
            };
        }
        public EmergencyContact MapperJsonToModel(RequestEmergencyContactJson emergencyContactJson)
        {
            return new EmergencyContact
            {
                Active = true,
                CreateDate = DateTimeController.DateTimeNow(),
                Name = emergencyContactJson.Name,
                DegreeOfKinship = emergencyContactJson.DegreeOfKinship,
                Phonenumbers = emergencyContactJson.Phonenumbers.Select(c => MapperJsonToModel(c)).ToList()
            };
        }
        public Phonenumber MapperJsonToModel(string number)
        {
            return new Phonenumber
            {
                Active = true,
                CreateDate = DateTimeController.DateTimeNow(),
                Number = number
            };
        }
        #endregion

        #region MapperModelToJson
        public ResponseLoginJson MapperModelToJsonLogin(User model)
        {
            return new ResponseLoginJson
            {
                Name = model.Name,
                IsPartOfOneHome = false,
                IsAdministrator = false
            };
        }
        public ResponseUserInformationsJson MapperModelToJson(User model)
        {
            return new ResponseUserInformationsJson
            {
                Name = model.Name,
                Email = model.Email,
                Phonenumbers = model.Phonenumbers.Select(c => MapperModelToJson(c)).ToList(),
                EmergencyContactc = model.EmergecyContacts.Select(c => MapperModelToJson(c)).ToList()
            };
        }
        public ResponsePhonenumberJson MapperModelToJson(Phonenumber model)
        {
            return new ResponsePhonenumberJson
            {
                Number = model.Number
            };
        }
        public ResponseEmergencyContactJson MapperModelToJson(EmergencyContact model)
        {
            return new ResponseEmergencyContactJson
            {
                Name = model.Name,
                DegreeOfKinship = model.DegreeOfKinship,
                Phonenumbers = model.Phonenumbers.Select(c => MapperModelToJson(c)).ToList()
            };
        }
        #endregion
    }
}