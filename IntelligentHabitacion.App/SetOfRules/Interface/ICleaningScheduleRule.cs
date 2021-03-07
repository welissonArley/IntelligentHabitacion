using IntelligentHabitacion.App.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface ICleaningScheduleRule
    {
        Task<object> GetMyTasks(DateTime? date = null);
        Task<ManageScheduleModel> GetSchedule();
        Task UpdateSchedule(ManageScheduleModel model);
        Task TaskCompletedToday(string id);
        Task<DetailsUserScheduleModel> GetDetailsAllTasksUserForAMonth(string id, DateTime date);
        Task<AllFriendsTasksModel> GetFriendsTasks(DateTime date);
        Task<int> RateFriendTask(RatingCleaningModel model);
        Task<IList<RatingCleaningModel>> GetRateTask(string taskId);
    }
}
