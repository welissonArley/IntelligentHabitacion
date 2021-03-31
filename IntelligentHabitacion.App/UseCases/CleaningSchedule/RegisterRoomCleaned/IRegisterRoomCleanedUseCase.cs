using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.RegisterRoomCleaned
{
    public interface IRegisterRoomCleanedUseCase
    {
        Task Execute(IList<string> taskIds, DateTime date);
    }
}
