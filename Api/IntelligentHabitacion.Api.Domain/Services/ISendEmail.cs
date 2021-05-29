using IntelligentHabitacion.Api.Domain.ValueObjects;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Services
{
    public interface ISendEmail
    {
        Task Send(EmailContent content);
        Task SendMessageSupport(EmailContent content);
    }
}
