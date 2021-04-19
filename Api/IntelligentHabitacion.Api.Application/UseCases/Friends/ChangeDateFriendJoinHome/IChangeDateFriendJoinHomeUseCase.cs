using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Friends.ChangeDateFriendJoinHome
{
    public interface IChangeDateFriendJoinHomeUseCase
    {
        Task<ResponseOutput> Execute(long myFriendId, RequestDateJson editMyFood);
    }
}
