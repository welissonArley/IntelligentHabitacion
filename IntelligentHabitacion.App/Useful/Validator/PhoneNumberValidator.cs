using IntelligentHabitacion.Exception;
using System.Text.RegularExpressions;

namespace IntelligentHabitacion.App.Useful.Validator
{
    public class PhoneNumberValidator
    {
        private readonly string regexExpression;

        public PhoneNumberValidator()
        {
            regexExpression = @"^\([1-9]{2}\) [1-9]{1} [0-9]{4}\-[0-9]{4}$";
        }

        public void IsValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new PhoneNumberEmptyException();

            Regex regex = new Regex(regexExpression);
            if (!regex.Match(phoneNumber).Success)
                throw new PhoneNumberInvalidException();
        }
    }
}
