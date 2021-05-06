using IntelligentHabitacion.Api.Domain.Services;
using Moq;

namespace Useful.ToTests.Builders.Email
{
    public class SendEmailBuilder
    {
        private static SendEmailBuilder _instance;
        private readonly Mock<ISendEmail> _repository;

        private SendEmailBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<ISendEmail>();
            }
        }

        public static SendEmailBuilder Instance()
        {
            _instance = new SendEmailBuilder();
            return _instance;
        }

        public ISendEmail Build()
        {
            return _repository.Object;
        }
    }
}
