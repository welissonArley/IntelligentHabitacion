using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseAllFriendsTasksScheduleJson
    {
        public ResponseAllFriendsTasksScheduleJson()
        {
            Tasks = new List<ResponseTasksForTheMonthJson>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileColor { get; set; }
        public List<ResponseTasksForTheMonthJson> Tasks { get; set; }
    }
}
