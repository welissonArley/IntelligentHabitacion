using IntelligentHabitacion.Api.Repository.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Repository.Model
{
    [Table("Phonenumber")]
    public class Phonenumber : ModelBase
    {
        public string Number { get; set; }
        public long UserId { get; set; }

        public override void Decrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Number = encryptManager.Dencrypt(Number, salt);
        }

        public override void Encrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Number = encryptManager.Encrypt(Number, salt);
        }
    }
}
