using IntelligentHabitacion.Api.Repository.Model;

namespace IntelligentHabitacion.Api.Repository.Cryptography
{
    public static class KeyModel
    {
        public static string GetKey(ModelBase model)
        {
            if (!((model as User) is null))
                return "AHRyipfROiWbUdoD";
            else if (!((model as Phonenumber) is null))
                return "SdhmzX8JgyLHoQin";
            else if (!((model as EmergencyContact) is null))
                return "AHRyipfROiWbUdoD";

            return "";
        }
    }
}
