using HashidsNet;
using System;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public abstract class ModelBase
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Active { get; set; }

        protected virtual string Salt()
        {
            return "xyjdiZb7ZM";
        }

        public virtual string EncryptedId()
        {
            return new Hashids(Salt()).EncodeLong(Id);
        }
        public virtual long DecryptedId(string encryptId)
        {
            return Convert.ToInt64(new Hashids(Salt()).DecodeLong(encryptId).FirstOrDefault());
        }

        public abstract void Encrypt();
        public abstract void Decrypt();
    }
}
