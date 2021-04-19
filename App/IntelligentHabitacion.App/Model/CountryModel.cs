using IntelligentHabitacion.App.Useful;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class CountryModel : ObservableObject
    {
        public CountryEnum Id { get; set; }
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public string Flag { get; set; }
    }
}
