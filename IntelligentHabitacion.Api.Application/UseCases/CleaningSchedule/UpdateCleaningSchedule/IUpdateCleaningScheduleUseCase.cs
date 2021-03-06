using IntelligentHabitacion.Communication.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.UpdateCleaningSchedule
{
    public interface IUpdateCleaningScheduleUseCase
    {
        Task<ResponseOutput> Execute(List<RequestUpdateCleaningScheduleJson> request);
    }
}
