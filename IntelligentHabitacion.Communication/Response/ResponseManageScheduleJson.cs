using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseManageScheduleJson
    {
        public ResponseManageScheduleJson()
        {
            UserTasks = new List<ResponseAllFriendsTasksScheduleJson>();
        }

        public IList<ResponseRoomJson> RoomsAvaliables { get; set; }
        public IList<ResponseAllFriendsTasksScheduleJson> UserTasks { get; set; }
    }
}
