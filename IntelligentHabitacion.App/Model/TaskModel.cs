using System.Collections.ObjectModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class TaskModel : ObservableObject
    {
        public string Id { get; set; }
        public string Room { get; set; }
        public bool CanRate { get; set; }
        public bool CanCompletedToday { get; set; }
        public ObservableCollection<FriendSimplifiedModel> Assign { get; set; }
    }
}
