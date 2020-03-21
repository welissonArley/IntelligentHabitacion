﻿using IntelligentHabitacion.Api.Repository.Model;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Interface
{
    public interface IBaseRepository<TModel> where TModel : ModelBase
    {
        IQueryable<TModel> GetAllActive();
        TModel GetById(long id);
        void Create(TModel model);
        void Update(TModel model);
        void DeleteOnDatabase(TModel model);
        void DeleteOnSystem(TModel model);
    }
}
