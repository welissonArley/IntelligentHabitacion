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
                Phonenumber = emergencyContactJson.Phonenumber
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
        public Home MapperJsonToModel(RequestRegisterHomeJson registerHomeJson)
        {
            return new Home
            {
                Active = true,
                CreateDate = DateTimeController.DateTimeNow(),
                Address = registerHomeJson.Address,
                City = registerHomeJson.City.Name,
                State = registerHomeJson.City.State.Name,
                Country = registerHomeJson.City.State.Country.Name,
                CountryAbbreviation = registerHomeJson.City.State.Country.Abbreviation,
                Complement = registerHomeJson.Complement,
                Neighborhood = registerHomeJson.Neighborhood,
                NetworksName = registerHomeJson.NetworksName,
                NetworksPassword = registerHomeJson.NetworksPassword,
                Number = registerHomeJson.Number,
                ZipCode = registerHomeJson.ZipCode
            };
        }
        #endregion

        #region MapperModelToJson
        public ResponseLoginJson MapperModelToJsonLogin(User model)
        {
            return new ResponseLoginJson
            {
                Name = model.Name,
                IsPartOfOneHome = model.Home != null,
                IsAdministrator = model.Home != null ? model.Home.AdministratorId == model.Id : false
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
                Phonenumber = model.Phonenumber
            };
        }
        #endregion
    }
}