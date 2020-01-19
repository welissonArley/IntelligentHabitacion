using XLabs.Data;

namespace IntelligentHabitacion.Model
{
    public class RegisterHomeModel : ObservableObject
    {
        public string ZipCode { get; set; }
        public CityModel City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public WifiNetworkModel NetWork { get; set; }
    }
}
