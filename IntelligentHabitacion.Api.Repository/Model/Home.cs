using IntelligentHabitacion.Api.Repository.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Repository.Model
{
    [Table("Home")]
    public class Home : ModelBase
    {
        private bool _encrypted { get; set; }

        public Home()
        {
            _encrypted = true;
        }

        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CountryAbbreviation { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string NetworksName { get; set; }
        public string NetworksPassword { get; set; }
        public long AdministratorId { get; set; }

        public override void Decrypt()
        {
            if (!_encrypted)
                return;

            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            ZipCode = encryptManager.Dencrypt(ZipCode, salt);
            City = encryptManager.Dencrypt(City, salt);
            State = encryptManager.Dencrypt(State, salt);
            Country = encryptManager.Dencrypt(Country, salt);
            CountryAbbreviation = encryptManager.Dencrypt(CountryAbbreviation, salt);
            Address = encryptManager.Dencrypt(Address, salt);
            Number = encryptManager.Dencrypt(Number, salt);
            Complement = encryptManager.Dencrypt(Complement, salt);
            Neighborhood = encryptManager.Dencrypt(Neighborhood, salt);
            NetworksName = encryptManager.Dencrypt(NetworksName, salt);
            NetworksPassword = encryptManager.Dencrypt(NetworksPassword, salt);

            _encrypted = false;
        }

        public override void Encrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            ZipCode = encryptManager.Encrypt(ZipCode, salt);
            City = encryptManager.Encrypt(City, salt);
            State = encryptManager.Encrypt(State, salt);
            Country = encryptManager.Encrypt(Country, salt);
            CountryAbbreviation = encryptManager.Encrypt(CountryAbbreviation, salt);
            Address = encryptManager.Encrypt(Address, salt);
            Number = encryptManager.Encrypt(Number, salt);
            Complement = encryptManager.Encrypt(Complement, salt);
            Neighborhood = encryptManager.Encrypt(Neighborhood, salt);
            NetworksName = encryptManager.Encrypt(NetworksName, salt);
            NetworksPassword = encryptManager.Encrypt(NetworksPassword, salt);

            _encrypted = true;
        }
    }
}
