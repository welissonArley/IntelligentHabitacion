using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.Friends.NotifyOrderReceived
{
    public interface INotifyOrderReceivedUseCase
    {
        Task Execute(string friendId);
    }
}
