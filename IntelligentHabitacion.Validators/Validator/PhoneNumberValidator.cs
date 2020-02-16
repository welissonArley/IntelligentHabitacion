using IntelligentHabitacion.Exception;
using System.Text.RegularExpressions;

namespace IntelligentHabitacion.Validators.Validator
{
    public class PhoneNumberValidator
    {
        public void IsValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new PhoneNumberEmptyException();

            Regex regex = new Regex(@"^\([1-9]{2}\) [1-9]{1} [0-9]{4}\-[0-9]{4}$");
            if (!regex.Match(phoneNumber).Success)
                throw new PhoneNumberInvalidException();
        }
    }
}
