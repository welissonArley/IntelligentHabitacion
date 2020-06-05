using IntelligentHabitacion.Api.Services.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IntelligentHabitacion.Api.Services.PushNotification
{
    public class OneSignalService : HttpClient, IPushNotificationService
    {
        private readonly string _key;
        private readonly string _appId;
        private readonly string _url;

        public OneSignalService(string appId, string key)
        {
            _key = key;
            _appId = appId;
            _url = "https://onesignal.com/api/v1/notifications";
        }

        public void Send(Dictionary<string, string> titleForEachLanguage, Dictionary<string, string> messageForEachLanguage, List<string> usersIds, Dictionary<string, string> data)
        {
            var bodyMensage = JsonConvert.SerializeObject(new
            {
                app_id = _appId,
                include_player_ids = usersIds,
                contents = messageForEachLanguage,
                headings = titleForEachLanguage,
                data
            });

            SendRequest(bodyMensage);
        }

        public void Send(Dictionary<string, string> titleForEachLanguage, Dictionary<string, string> messageForEachLanguage, List<string> usersIds)
        {
            var bodyMensage = JsonConvert.SerializeObject(new
            {
                app_id = _appId,
                include_player_ids = usersIds,
                contents = messageForEachLanguage,
                headings = titleForEachLanguage
            });

            SendRequest(bodyMensage);
        }

        private void SendRequest(string body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _url);

            request.Headers.Add("Authorization", "Basic " + _key);

            request.Content = new StringContent(body, Encoding.UTF8, "application/json");

            var tarefa = SendAsync(request);
            tarefa.Wait();
        }
    }
}
