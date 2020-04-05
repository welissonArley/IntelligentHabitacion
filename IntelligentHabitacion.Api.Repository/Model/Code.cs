using IntelligentHabitacion.Api.Repository.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public enum CodeType
    {
        ResetPassword = 1
    }

    [Table("Code")]
    public class Code : ModelBase
    {
        public string Value { get; set; }
        public CodeType Type { get; set; }
        public long UserId { get; set; }

        public override void Decrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Value = encryptManager.Dencrypt(Value, salt);
        }

        public override void Encrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Value = encryptManager.Encrypt(Value, salt);
        }
    }
}
