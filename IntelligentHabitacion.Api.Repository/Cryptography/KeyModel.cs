using IntelligentHabitacion.Api.Repository.Model;

namespace IntelligentHabitacion.Api.Repository.Cryptography
{
    public static class KeyModel
    {
        public static string GetKey(ModelBase model)
        {
            if (!((model as User) is null))
                return "AHRyipfROiWbUdoD";
            
            if (!((model as Phonenumber) is null))
                return "SdhmzX8JgyLHoQin";
            
            if (!((model as EmergencyContact) is null))
                return "AHRyipfROiWbUdoD";

            if (!((model as Code) is null))
                return "0Xb9EqlbJbT9WGSb";

            if (!((model as Home) is null))
                return "U7L5atxzMZbXzFEt";

            if (!((model as MyFood) is null))
                return "3RAGAaHTWK8pGSJU";

            return "";
        }

        //Use for token
        private const string TokenKey = "1M0okZY8u4EmqGjO";

        public static string GetKey()
        {
            return TokenKey;
        }
    }
}
