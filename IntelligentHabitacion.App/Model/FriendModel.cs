using System;
using System.Collections.Generic;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class FriendModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> Phonenumbers { get; set; }
        public IList<EmergencyContactModel> EmergencyContacts { get; set; }
        public string ProfileColor { get; set; }
        public DateTime JoinedOn { get; set; }
        public string DescriptionDateJoined { get; set; }
    }
}
