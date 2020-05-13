using System;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class AcceptNewFriendModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal RentAmount { get; set; }
        public string ProfileColor { get; set; }
    }
}
