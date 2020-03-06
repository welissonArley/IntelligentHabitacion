using System;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public abstract class ModelBase
    {
        public virtual long Id { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
        public virtual bool Active { get; set; }

        public abstract void Encripty();
        public abstract void Decrypt();
    }
}
