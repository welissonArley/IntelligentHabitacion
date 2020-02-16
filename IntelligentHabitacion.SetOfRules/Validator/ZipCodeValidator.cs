using IntelligentHabitacion.Exception;
using System.Text.RegularExpressions;

namespace IntelligentHabitacion.SetOfRules.Validator
{
    public class ZipCodeValidator
    {
        public void IsValid(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ZipCodeEmptyException();

            Regex regex = new Regex(@"^[0-9]{2}\.[0-9]{3}\-[0-9]{3}$");
            if (!regex.Match(zipCode).Success)
                throw new ZipCodeInvalidException();
        }
    }
}