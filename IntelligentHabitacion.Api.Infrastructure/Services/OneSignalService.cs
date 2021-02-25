using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Api.Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IntelligentHabitacion.Api.Infrastructure.Services
{
    public class OneSignalService : HttpClient, IPushNotificationService
    {
        private readonly string _key;
        private readonly string _appId;
        private readonly string _url;

        public OneSignalService(OneSignalConfig config)
        {
            _key = config.Key;
            _appId = config.AppId;
            _url = config.Url;
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
            var body = new MessageBodyOneSignal
            {
                app_id = _appId,
                include_player_ids = usersIds,
                contents = messageForEachLanguage,
                headings = titleForEachLanguage
            };

            if (time.HasValue)
            {
                body.delayed_option = "timezone";
                body.delivery_time_of_day = time.Value.ToString("HH:mm");
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
            tarefa.ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
