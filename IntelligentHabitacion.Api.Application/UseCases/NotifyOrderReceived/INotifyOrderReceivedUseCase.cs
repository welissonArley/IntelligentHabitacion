using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.NotifyOrderReceived
{
    public interface INotifyOrderReceivedUseCase
    {
        Task<ResponseOutput> Execute(long friendId);
    }
}
