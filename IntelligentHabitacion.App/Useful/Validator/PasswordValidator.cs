using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.App.Useful.Validator
{
    public class PasswordValidator
    {
        public void IsValidaPasswordAndConfirmation(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();

            if (password.Length < 6)
                throw new InvalidPasswordException();
        }
    }
}
