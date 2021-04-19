using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.HistoryOfTheDay
{
    public interface IHistoryOfTheDayUseCase
    {
        Task<ResponseOutput> Execute(RequestHistoryOfTheDayJson request);
    }
}
