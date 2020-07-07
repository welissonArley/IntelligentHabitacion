using IntelligentHabitacion.Exception;
using System.Text.RegularExpressions;

namespace IntelligentHabitacion.Validators.Validator
{
    public class ZipCodeValidator
    {
        private readonly string regexExpression;

        public ZipCodeValidator()
        {
            regexExpression = @"^[0-9]{2}\.[0-9]{3}\-[0-9]{3}$";
        }

        public void IsValid(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ZipCodeEmptyException();

            Regex regex = new Regex(regexExpression);
            if (!regex.Match(zipCode).Success)
                throw new ZipCodeInvalidException();
        }
    }
}
