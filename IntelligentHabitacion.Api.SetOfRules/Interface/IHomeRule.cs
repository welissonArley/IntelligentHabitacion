using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IHomeRule
    {
        void Register(RequestRegisterHomeJson registerHomeJson);
    }
}
