using IntelligentHabitacion.Api.Services.Interface;
using Newtonsoft.Json;
using System;
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

        public void Send(Dictionary<string, string> titleForEachLanguage, Dictionary<string, string> messageForEachLanguage, List<string> usersIds, DateTime? time = null)
        {
            var body = new MessageBody
            {
                app_id = _appId,
                include_player_ids = usersIds,
                contents = messageForEachLanguage,
                headings = titleForEachLanguage
            };
            
            if (time.HasValue)
            {
                body.delayed_option = "timezone";
                body.delivery_time_of_day = time.Value.ToString("HH:mm:ss");
            }

            var bodyMensage = JsonConvert.SerializeObject(body);

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

    public class MessageBody
    {
        public string app_id { get; set; }
        public List<string> include_player_ids { get; set; }
        public Dictionary<string, string> contents { get; set; }
        public Dictionary<string, string> headings { get; set; }
        public string delayed_option { get; set; }
        public string delivery_time_of_day { get; set; }
    }
}
