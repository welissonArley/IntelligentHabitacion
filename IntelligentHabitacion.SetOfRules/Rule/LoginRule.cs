using IntelligentHabitacion.Exception;
using IntelligentHabitacion.SetOfRules.Interface;
using IntelligentHabitacion.SetOfRules.Validator;

namespace IntelligentHabitacion.SetOfRules.Rule
{
    public class LoginRule : ILoginRule
    {
        public void ChangePasswordForgetPassword(string code, string newPassword, string confirmationPassword)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new CodeEmptyException();

            if (string.IsNullOrWhiteSpace(newPassword))
                throw new PasswordEmptyException();

            if (!newPassword.Equals(confirmationPassword))
                throw new PasswordIsNotSameConfirmationException();
        }

        public void RequestCode(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new EmailEmptyException();

            if(!new EmailValidator().IsValidEmail(email))
                throw new EmailInvalidException();
        }
    }
}
