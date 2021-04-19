using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Friends.GetMyFriends
{
    public interface IGetMyFriendsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
