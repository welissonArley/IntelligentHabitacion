using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IHomeRule
    {
        void Register(RequestUpdateHomeJson registerHomeJson);
        ResponseHomeInformationsJson GetInformations();
        void Update(RequestUpdateHomeJson updateHomeJson);
        void Delete(RequestAdminActionJson request);
        void RequestCodeDeleteHome();
    }
}
