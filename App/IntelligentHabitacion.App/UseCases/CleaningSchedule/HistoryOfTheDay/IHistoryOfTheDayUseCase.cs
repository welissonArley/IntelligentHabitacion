using IntelligentHabitacion.App.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.HistoryOfTheDay
{
    public interface IHistoryOfTheDayUseCase
    {
        Task<IList<DetailsTaskCleanedOnDayModelGroup>> Execute(DateTime date, string room = null);
    }
}
