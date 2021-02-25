using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Domain.Services
{
    public interface IPushNotificationService
    {
        void Send(Dictionary<string, string> titleForEachLanguage, Dictionary<string, string> messageForEachLanguage, List<string> usersIds, DateTime? time = null);
        void Send(Dictionary<string, string> titleForEachLanguage, Dictionary<string, string> messageForEachLanguage, List<string> usersIds, Dictionary<string, string> data);
    }
}
