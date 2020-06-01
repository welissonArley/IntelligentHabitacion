using System;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class FriendModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phonenumber1 { get; set; }
        public string Phonenumber2 { get; set; }
        public EmergencyContactModel EmergencyContact1 { get; set; }
        public EmergencyContactModel EmergencyContact2 { get; set; }
        public string ProfileColor { get; set; }
        public DateTime JoinedOn { get; set; }
        public string DescriptionDateJoined { get; set; }
    }
}
