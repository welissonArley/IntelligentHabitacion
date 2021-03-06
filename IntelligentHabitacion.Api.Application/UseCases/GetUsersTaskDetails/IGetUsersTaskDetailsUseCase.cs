using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.GetUsersTaskDetails
{
    public interface IGetUsersTaskDetailsUseCase
    {
        Task<ResponseOutput> Execute(long userId, DateTime date);
    }
}
