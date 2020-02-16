using XLabs.Data;

namespace IntelligentHabitacion.Model
{
    public class EmergencyContactModel : ObservableObject
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string FamilyRelationship { get; set; }
    }
}
