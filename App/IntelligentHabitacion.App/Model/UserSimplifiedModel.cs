using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class UserSimplifiedModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileColor { get; set; }
    }
}
