using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IUserRule
    {
        BooleanJson EmailAlreadyBeenRegistered(string email);
        void Update(RequestUpdateUserJson updateUserJson);
        void ChangePassword(RequestChangePasswordJson changePasswordJson);
        ResponseUserInformationsJson GetInformations();
    }
}
