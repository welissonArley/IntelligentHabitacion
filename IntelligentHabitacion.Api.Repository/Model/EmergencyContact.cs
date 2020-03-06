using IntelligentHabitacion.Api.Repository.Cryptography;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public class EmergencyContact : ModelBase
    {
        public virtual string Name { get; set; }
        public virtual string DegreeOfKinship { get; set; }
        public virtual ICollection<Phonenumber> Phonenumbers { get; set; }

        public override void Decrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Dencrypt(Name, salt);
            DegreeOfKinship = encryptManager.Dencrypt(DegreeOfKinship, salt);
            foreach (var phoneNumber in Phonenumbers)
                phoneNumber.Decrypt();
        }

        public override void Encripty()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Encrypt(Name, salt);
            DegreeOfKinship = encryptManager.Encrypt(DegreeOfKinship, salt);
            foreach (var phoneNumber in Phonenumbers)
                phoneNumber.Encripty();
        }
    }
}
