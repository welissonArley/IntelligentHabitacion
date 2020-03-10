using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.Validators.Validator
{
    public class EmailValidator
    {
        public void IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new EmailEmptyException();

            try
            {
                _ = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                throw new EmailInvalidException();
            }
        }
    }
}
