using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IHomeRule
    {
        void Register(RequestHomeJson registerHomeJson);
        ResponseHomeInformationsJson GetInformations();
        void Update(RequestHomeJson updateHomeJson);
    }
}
