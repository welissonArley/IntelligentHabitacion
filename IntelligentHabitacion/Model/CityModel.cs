using XLabs.Data;

namespace IntelligentHabitacion.Model
{
    public class CityModel : ObservableObject
    {
        public CityModel()
        {
            State = new StateModel();
        }

        public string Name { get; set; }
        public StateModel State { get; set; }
    }
}
