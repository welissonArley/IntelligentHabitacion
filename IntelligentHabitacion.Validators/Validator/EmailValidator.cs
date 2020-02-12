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
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                    throw new EmailInvalidException();
            }
            catch
            {
                throw new EmailInvalidException();
            }
        }
    }
}
