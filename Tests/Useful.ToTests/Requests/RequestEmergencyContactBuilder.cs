using Bogus;
using IntelligentHabitacion.Communication.Request;

namespace Useful.ToTests.Requests
{
    public class RequestEmergencyContactBuilder
    {
        private static RequestEmergencyContactBuilder _instance;

        public static RequestEmergencyContactBuilder Instance()
        {
            _instance = new RequestEmergencyContactBuilder();
            return _instance;
        }

        public RequestEmergencyContactJson Build()
        {
            return new Faker<RequestEmergencyContactJson>()
                .RuleFor(u => u.Name, (f) => f.Name.FullName())
                .RuleFor(u => u.Relationship, (_) => "Mother")
                .RuleFor(u => u.Phonenumber, (f) => f.Person.Phone);
        }
    }
}
