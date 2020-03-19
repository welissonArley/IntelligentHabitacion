using IntelligentHabitacion.Api.Repository.Cryptography;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Repository.Model
{
    [Table("User")]
    public class User : ModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Phonenumber> Phonenumbers { get; set; }
        public ICollection<EmergencyContact> EmergecyContacts { get; set; }

        public override void Decrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Email = encryptManager.Dencrypt(Email, salt);
            Password = encryptManager.Dencrypt(Password, salt);
            foreach (var phoneNumber in Phonenumbers)
                phoneNumber.Decrypt();
            foreach (var emergencyContact in EmergecyContacts)
                emergencyContact.Decrypt();
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
        }
    }
}
