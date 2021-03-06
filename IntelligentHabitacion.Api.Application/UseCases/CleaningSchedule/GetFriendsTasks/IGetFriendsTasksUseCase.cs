using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetFriendsTasks
{
    public interface IGetFriendsTasksUseCase
    {
        Task<ResponseOutput> Execute(DateTime date);
    }
}
