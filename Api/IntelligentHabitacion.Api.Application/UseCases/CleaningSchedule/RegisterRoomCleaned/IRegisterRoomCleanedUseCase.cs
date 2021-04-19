using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.RegisterRoomCleaned
{
    public interface IRegisterRoomCleanedUseCase
    {
        Task<ResponseOutput> Execute(RequestRegisterRoomCleaned request);
    }
}
