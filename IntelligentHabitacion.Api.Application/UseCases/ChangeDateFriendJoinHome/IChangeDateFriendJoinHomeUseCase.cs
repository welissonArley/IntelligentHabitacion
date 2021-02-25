using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangeDateFriendJoinHome
{
    public interface IChangeDateFriendJoinHomeUseCase
    {
        Task<ResponseOutput> Execute(long myFriendId, RequestChangeDateJoinHomeJson editMyFood);
    }
}
