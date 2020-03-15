using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface ILoginRule
    {
        ResponseLoginJson DoLogin(RequestLoginJson loginJson);
    }
}
