using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class SelectOptionModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}
