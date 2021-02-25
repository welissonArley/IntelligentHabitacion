using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.GetMyFriends
{
    public interface IGetMyFriendsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
