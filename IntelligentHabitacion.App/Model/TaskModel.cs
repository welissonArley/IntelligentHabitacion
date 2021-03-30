using System.Collections.ObjectModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class TaskModel : ObservableObject
    {
        public TaskModel()
        {
            Assign = new ObservableCollection<UserSimplifiedModel>();
        }

        public string Room { get; set; }
        public bool CanRate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanCompletedToday { get; set; }
        public ObservableCollection<UserSimplifiedModel> Assign { get; set; }
    }
}
