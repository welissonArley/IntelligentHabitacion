using XLabs.Data;

namespace IntelligentHabitacion.Model
{
    public class CityModel : ObservableObject
    {
        public string Name { get; set; }
        public StateModel State { get; set; }
    }
}
