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

            return "";
        }

        //Use for token
        public static string GetKey()
        {
            return "1M0okZY8u4EmqGjO";
        }
    }
}
