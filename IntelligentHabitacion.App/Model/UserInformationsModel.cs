﻿using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class UserInformationsModel : ObservableObject
    {
        public string Name { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public EmergencyContactModel EmergencyContact1 { get; set; }
        public EmergencyContactModel EmergencyContact2 { get; set; }
    }
}
