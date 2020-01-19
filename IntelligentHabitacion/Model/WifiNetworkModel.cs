using XLabs.Data;

namespace IntelligentHabitacion.Model
{
    public class WifiNetworkModel : ObservableObject
    {
        public string NetworkName { get; set; }
        public string Password { get; set; }
    }
}
