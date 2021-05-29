using Bogus;
using IntelligentHabitacion.Communication.Request;
using System;
using System.Collections.Generic;

namespace Useful.ToTests.Requests
{
    public class RequestRegisterUserBuilder
    {
        private static RequestRegisterUserBuilder _instance;

        public static RequestRegisterUserBuilder Instance()
        {
            _instance = new RequestRegisterUserBuilder();
            return _instance;
        }

        public RequestRegisterUserJson Build()
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(u => u.Name, (f) => f.Name.FullName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Password, (f) => f.Internet.Password(10))
                .RuleFor(u => u.PushNotificationId, (_) => Guid.NewGuid().ToString())
                .RuleFor(u => u.Phonenumbers, (f) => new List<string> { f.Person.Phone })
                .RuleFor(u => u.EmergencyContacts, () => CreateEmergencyContacts());
        }

        private IList<RequestEmergencyContactJson> CreateEmergencyContacts()
        {
            return new List<RequestEmergencyContactJson>
            {
                RequestEmergencyContactBuilder.Instance().Build(),
                RequestEmergencyContactBuilder.Instance().Build()
            };
        }
    }
}
