using IntelligentHabitacion.Api.SetOfRules.Cryptography;

namespace IntelligentHabitacion.Api.Test.FactoryFake
{
    public abstract class BaseFactoryFake
    {
        protected CryptographyPassword GetCryptographyPassword()
        {
            return new CryptographyPassword("");
        }
    }
}
