using System.Collections.Generic;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseCreateScheduleCleaningHouseJson
    {
        public ResponseCreateScheduleCleaningHouseJson()
        {
            Rooms = new List<ResponseRoomJson>();
            Friends = new List<ResponseFriendSimplifiedJson>();
        }

        public IList<ResponseRoomJson> Rooms { get; set; }
        public IList<ResponseFriendSimplifiedJson> Friends { get; set; }
    }
}
