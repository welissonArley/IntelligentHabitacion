using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IUserRule
    {
        void Register(RequestRegisterUserJson registerUserJson);
    }
}
