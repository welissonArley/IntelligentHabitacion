using Com.OneSignal.Abstractions;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.Useful;
using System.Linq;

namespace IntelligentHabitacion.App.OneSignalConfig
{
    public static class OneSignalManager
    {
        public static string OneSignalKey { get { return "658a8e23-65fe-450f-9bf8-9ef1c3d1abdc"; } }
        public static string MyOneSignalId { private set; get; }

        public static void SetMyIdOneSignal(string myId)
        {
            MyOneSignalId = myId;
        }

        public static void Notification(OSNotification notification)
        {
            var database = XLabs.Ioc.Resolver.Resolve<ISqliteDatabase>();
            var key = notification.payload.additionalData.Keys.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(key))
                return;

            switch (key)
            {
                case EnumNotifications.OrderReceived:
                    {
                        database.ReceivedOrder();
                    }break;
            }
        }
    }
}
