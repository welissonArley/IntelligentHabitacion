using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Services.WebSocket
{
    public static class Manager
    {
        private static Dictionary<string, object> _dictionary;

        public static void Add(string adminConnectionSocketId, string adminId, Context context)
        {
            if (_dictionary == null)
                _dictionary = new Dictionary<string, object>();

            _dictionary.Add(adminConnectionSocketId, adminId);
            _dictionary.Add(adminId, context);
        }

        public static async Task Remove(string adminConnectionSocketId)
        {
            if (_dictionary.ContainsKey(adminConnectionSocketId))
            {
                var adminId = _dictionary[adminConnectionSocketId].ToString();
                var context = (Context)_dictionary[adminId];
                context.StopTimer();
                await context.SendErrorConnectionLostFriendCandidate();
                _dictionary.Remove(adminConnectionSocketId);
                _dictionary.Remove(adminId);
            }
        }

        public static Context Get(string adminId)
        {
            return (Context)_dictionary[adminId];
        }

        public static string GetAdminId(string adminConnectionSocketId)
        {
            return _dictionary.ContainsKey(adminConnectionSocketId) ? _dictionary[adminConnectionSocketId].ToString() : null;
        }
    }
}
