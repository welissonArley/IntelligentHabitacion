using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class StateModel : ObservableObject
    {
        public StateModel()
        {
            Country = new CountryModel();
        }

        public string Name { get; set; }
        public CountryModel Country { get; set; }
    }
}
