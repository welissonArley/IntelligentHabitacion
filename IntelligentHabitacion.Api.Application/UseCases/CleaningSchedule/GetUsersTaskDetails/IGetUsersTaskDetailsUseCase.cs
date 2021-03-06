using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetUsersTaskDetails
{
    public interface IGetUsersTaskDetailsUseCase
    {
        Task<ResponseOutput> Execute(long userId, DateTime date);
    }
}
