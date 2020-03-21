using IntelligentHabitacion.Api.Repository.Cryptography;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Repository.Model
{
    [Table("EmergencyContact")]
    public class EmergencyContact : ModelBase
    {
        public string Name { get; set; }
        public string DegreeOfKinship { get; set; }
        public ICollection<Phonenumber> Phonenumbers { get; set; }
        public long UserId { get; set; }

        public override void Decrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Dencrypt(Name, salt);
            DegreeOfKinship = encryptManager.Dencrypt(DegreeOfKinship, salt);
            foreach (var phoneNumber in Phonenumbers)
                phoneNumber.Decrypt();
        }

        public override void Encrypty()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Encrypt(Name, salt);
            DegreeOfKinship = encryptManager.Encrypt(DegreeOfKinship, salt);
            foreach (var phoneNumber in Phonenumbers)
                phoneNumber.Encrypty();
        }
    }
}
