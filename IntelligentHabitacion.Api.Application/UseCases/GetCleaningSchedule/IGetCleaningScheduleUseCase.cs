using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.GetCleaningSchedule
{
    public interface IGetCleaningScheduleUseCase
    {
        Task<ResponseOutput> Execute(DateTime date);
    }
}
