using IntelligentHabitacion.Api.Repository.Cryptography;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public class Phonenumber : ModelBase
    {
        public virtual string Number { get; set; }

        public override void Decrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Number = encryptManager.Dencrypt(Number, salt);
        }

        public override void Encripty()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Number = encryptManager.Encrypt(Number, salt);
        }
    }
}
