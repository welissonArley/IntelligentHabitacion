using IntelligentHabitacion.Api.Repository.Cryptography;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public class User : ModelBase
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual ICollection<Phonenumber> Phonenumbers { get; set; }
        public virtual ICollection<EmergencyContact> EmergecyContacts { get; set; }

        public override void Decrypt()
        {
            if (!Encripted)
                return;

            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Email = encryptManager.Dencrypt(Email, salt);
            Password = encryptManager.Dencrypt(Password, salt);
            foreach (var phoneNumber in Phonenumbers)
                phoneNumber.Decrypt();
            foreach (var emergencyContact in EmergecyContacts)
                emergencyContact.Decrypt();

            Encripted = false;
        }

        public override void Encripty()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Email = encryptManager.Encrypt(Email, salt);
            Password = encryptManager.Encrypt(Password, salt);
            foreach (var phoneNumber in Phonenumbers)
                phoneNumber.Encripty();
            foreach (var emergencyContact in EmergecyContacts)
                emergencyContact.Encripty();

            Encripted = true;
        }

        private bool Encripted = true;
    }
}
