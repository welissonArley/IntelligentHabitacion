using System;
using System.Collections.ObjectModel;

namespace IntelligentHabitacion.App.Model
{
    public class AllFriendsTasksModel 
    {
        public DateTime Month { get; set; }
        public ObservableCollection<AllFriendsGroup> FriendsTasks { get; set; }
    }

    public class AllFriendsGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileColor { get; set; }
        public ObservableCollection<TasksForTheMonth> Tasks { get; set; }
    }
}
