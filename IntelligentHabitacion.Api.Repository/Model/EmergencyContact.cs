using IntelligentHabitacion.Api.Repository.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Repository.Model
{
    [Table("EmergencyContact")]
    public class EmergencyContact : ModelBase
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Phonenumber { get; set; }
        public long UserId { get; set; }

        public override void Decrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Dencrypt(Name, salt);
            Relationship = encryptManager.Dencrypt(Relationship, salt);
            Phonenumber = encryptManager.Dencrypt(Phonenumber, salt);
        }

        public override void Encrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Encrypt(Name, salt);
            Relationship = encryptManager.Encrypt(Relationship, salt);
            Phonenumber = encryptManager.Encrypt(Phonenumber, salt);
        }
    }
}
