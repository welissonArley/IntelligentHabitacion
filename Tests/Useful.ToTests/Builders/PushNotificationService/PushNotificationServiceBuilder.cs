using IntelligentHabitacion.Api.Domain.Services;
using Moq;

namespace Useful.ToTests.Builders.PushNotificationService
{
    public class PushNotificationServiceBuilder
    {
        private static PushNotificationServiceBuilder _instance;
        private readonly Mock<IPushNotificationService> _service;

        private PushNotificationServiceBuilder()
        {
            if (_service == null)
            {
                _service = new Mock<IPushNotificationService>();
            }
        }

        public static PushNotificationServiceBuilder Instance()
        {
            _instance = new PushNotificationServiceBuilder();
            return _instance;
        }

        public IPushNotificationService Build()
        {
            return _service.Object;
        }
    }
}
