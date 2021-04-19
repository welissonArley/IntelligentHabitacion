using IntelligentHabitacion.Communication.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.CreateFirstSchedule
{
    public interface ICreateFirstScheduleUseCase
    {
        Task<ResponseOutput> Execute(IList<RequestUpdateCleaningScheduleJson> request);
    }
}
