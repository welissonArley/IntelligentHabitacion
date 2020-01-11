using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.SetOfRules.Validator
{
    public class PasswordValidator
    {
        public void IsValidaPasswordAndConfirmation(string password, string passwordConfirmation)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();

            if (!password.Equals(passwordConfirmation))
                throw new PasswordIsNotSameConfirmationException();

            if (password.Length < 6)
                throw new InvalidPasswordException();
        }
    }
}
