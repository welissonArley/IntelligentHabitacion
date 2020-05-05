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
                Phonenumbers = userJson.Phonenumbers.Select(c => MapperJsonToModel(c)).ToList(),
                ProfileColor = Color.RandomColor()
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
        public Home MapperJsonToModel(RequestHomeJson registerHomeJson)
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
        public MyFood MapperJsonToModel(RequestAddMyFoodJson model)
        {
            return new MyFood
            {
                Active = true,
                Amount = model.Amount,
                DueDate = model.DueDate,
                Manufacturer = model.Manufacturer,
                Name = model.Name,
                Type = (Repository.Model.Type)model.Type,
                CreateDate = DateTimeController.DateTimeNow()
            };
        }
        #endregion

        #region MapperModelToJson
        public ResponseLoginJson MapperModelToJsonLogin(User model)
        {
            var response = new ResponseLoginJson
            {
                Name = model.Name,
                IsPartOfOneHome = model.HomeAssociationId != null,
                IsAdministrator = false
            };

            if (model.HomeAssociation != null)
                response.IsAdministrator = model.HomeAssociation.Home.AdministratorId == model.Id;

            return response;
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
        public ResponseHomeInformationsJson MapperModelToJson(Home model)
        {
            return new ResponseHomeInformationsJson
            {
                Address = model.Address,
                City = model.City,
                Complement = model.Complement,
                Neighborhood = model.Neighborhood,
                Number = model.Number,
                ZipCode = model.ZipCode,
                NetWork = new ResponseWifiNetworkJson
                {
                    Name = model.NetworksName,
                    Password = model.NetworksPassword
                },
                State = new ResponseStateJson
                {
                    Name = model.State,
                    Country = new ResponseCountryJson
                    {
                        Name = model.Country,
                        Abbreviation = model.CountryAbbreviation
                    }
                }
            };
        }
        public ResponseFriendJson MapperModelToJsonFriend(User model)
        {
            return new ResponseFriendJson
            {
                Name = model.Name,
                Phonenumbers = model.Phonenumbers.Select(c => MapperModelToJson(c)).ToList(),
                EmergencyContact = model.EmergecyContacts.Select(c => MapperModelToJson(c)).ToList(),
                ProfileColor = model.ProfileColor,
                JoinedOn = model.HomeAssociation.JoinedOn
            };
        }
        public ResponseMyFoodJson MapperModelToJson(MyFood model)
        {
            return new ResponseMyFoodJson
            {
                Name = model.Name,
                Amount = model.Amount,
                DueDate = model.DueDate,
                Manufacturer = model.Manufacturer,
                Type = (Communication.Response.Type)model.Type,
                Id = model.EncryptedId()
            };
        }
        #endregion
    }
}