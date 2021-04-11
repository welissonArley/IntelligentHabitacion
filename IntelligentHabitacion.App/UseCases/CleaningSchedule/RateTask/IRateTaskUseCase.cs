using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.RateTask
{
    public interface IRateTaskUseCase
    {
        Task<int> Execute(RateTaskModel model);
    }
}
