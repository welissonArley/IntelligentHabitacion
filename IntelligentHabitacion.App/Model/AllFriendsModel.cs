using System;
using System.Collections.ObjectModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class AllFriendsTasksModel : ObservableObject
    {
        public DateTime Month { get; set; }
        public ObservableCollection<AllFriendsGroup> FriendsTasks { get; set; }
    }

    public class AllFriendsGroup : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileColor { get; set; }
        public ObservableCollection<TasksForTheMonth> Tasks { get; set; }
    }
}
