using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Useful;
using System;
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
                ProfileColor = Color.RandomColor(),
                PushNotificationId = userJson.PushNotificationId
            };
        }
        public EmergencyContact MapperJsonToModel(RequestEmergencyContactJson emergencyContactJson)
        {
            return new EmergencyContact
            {
                Active = true,
                CreateDate = DateTimeController.DateTimeNow(),
                Name = emergencyContactJson.Name,
                Relationship = emergencyContactJson.Relationship,
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
                AdditionalAddressInfo = registerHomeJson.AdditionalAddressInfo,
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
                Quantity = model.Quantity,
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
                ProfileColor = model.ProfileColor,
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
                EmergencyContacts = model.EmergecyContacts.Select(c => MapperModelToJson(c)).ToList()
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
                Relationship = model.Relationship,
                Phonenumber = model.Phonenumber
            };
        }
        public ResponseHomeInformationsJson MapperModelToJson(Home model)
        {
            return new ResponseHomeInformationsJson
            {
                Address = model.Address,
                City = model.City,
                AdditionalAddressInfo = model.AdditionalAddressInfo,
                Neighborhood = model.Neighborhood,
                Number = model.Number,
                ZipCode = model.ZipCode,
                NetWork = new ResponseWifiNetworkJson
                {
                    Name = model.NetworksName,
                    Password = model.NetworksPassword
                },
                StateProvince = model.StateProvince
            };
        }
        public ResponseFriendJson MapperModelToJsonFriend(User model, DateTime requestersJoinedOn)
        {
            return new ResponseFriendJson
            {
                Id = model.EncryptedId(),
                Name = model.Name,
                Phonenumbers = model.Phonenumbers.Select(c => MapperModelToJson(c)).ToList(),
                EmergencyContact = model.EmergecyContacts.Select(c => MapperModelToJson(c)).ToList(),
                ProfileColor = model.ProfileColor,
                JoinedOn = model.HomeAssociation.JoinedOn,
                DescriptionDateJoined = string.Format(ResourceText.DESCRIPTION_DATE_JOINED_THE_HOUSE, DateTimeController.DateToStringYearMonthAndDay(DateTime.Compare(requestersJoinedOn, model.HomeAssociation.JoinedOn) == 1 ? requestersJoinedOn : model.HomeAssociation.JoinedOn))
            };
        }
        public ResponseMyFoodJson MapperModelToJson(MyFood model)
        {
            return new ResponseMyFoodJson
            {
                Name = model.Name,
                Quantity = model.Quantity,
                DueDate = model.DueDate,
                Manufacturer = model.Manufacturer,
                Type = (Communication.Response.Type)model.Type,
                Id = model.EncryptedId()
            };
        }
        #endregion
    }
}