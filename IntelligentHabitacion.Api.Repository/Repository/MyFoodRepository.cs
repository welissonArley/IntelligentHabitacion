using IntelligentHabitacion.Api.Repository.DatabaseInformations;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public class MyFoodRepository : BaseRepository<MyFood>, IMyFoodRepository
    {
        public MyFoodRepository(IDatabaseInformations databaseInformations) : base(databaseInformations)
        {
        }

        private IQueryable<MyFood> IncludeModel()
        {
            return ModelSet.Where(c => c.Active);
        }

        public MyFood GetMyFood(long foodId, long userId)
        {
            var model = IncludeModel().FirstOrDefault(c => c.UserId == userId && c.Id == foodId);
            model?.Decrypt();

            return model;
        }

        public List<MyFood> GetMyFoods(long userId)
        {
            var models = IncludeModel().Where(c => c.UserId == userId);
            foreach (var model in models)
                model.Decrypt();

            return models.ToList();
        }

        public List<MyFood> GetExpiredOrCloseToDueDate()
        {
            var today = DateTime.UtcNow.Date;

            var models = IncludeModel().AsNoTracking().Where(c => c.DueDate.HasValue)
                .AsEnumerable().Where(c => (c.DueDate.Value - today).TotalDays == 7
                || (c.DueDate.Value - today).TotalDays == 3
                || (c.DueDate.Value - today).TotalDays <= 1);

            foreach (var model in models)
                model.Decrypt();

            return models.ToList();
        }
    }
}
