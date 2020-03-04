using System;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public class ModelBase
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Active { get; set; }
    }
}
