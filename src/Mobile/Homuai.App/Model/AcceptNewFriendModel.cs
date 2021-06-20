using System;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class AcceptNewFriendModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public string ProfileColor { get; set; }
    }
}
