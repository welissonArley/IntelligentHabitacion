using IntelligentHabitacion.Api.Domain.ValueObjects;

namespace IntelligentHabitacion.Api.Domain.Services
{
    public interface ISendEmail
    {
        void Send(EmailContent content);
    }
}
