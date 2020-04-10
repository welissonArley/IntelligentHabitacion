using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class HomeModel : ObservableObject
    {
        public HomeModel()
        {
            City = new CityModel();
            NetWork = new WifiNetworkModel();
        }
        public string ZipCode { get; set; }
        public CityModel City { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public WifiNetworkModel NetWork { get; set; }
    }
}
