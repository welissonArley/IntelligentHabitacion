using XLabs.Data;

namespace IntelligentHabitacion.Model
{
    public class StateModel : ObservableObject
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public CountryModel Country { get; set; }
    }
}
