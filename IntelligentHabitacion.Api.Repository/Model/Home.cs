using IntelligentHabitacion.Api.Repository.Cryptography;
using IntelligentHabitacion.Useful;
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
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalAddressInfo { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public short DeadlinePaymentRent { get; set; }
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
            StateProvince = encryptManager.Dencrypt(StateProvince, salt);
            Address = encryptManager.Dencrypt(Address, salt);
            Number = encryptManager.Dencrypt(Number, salt);
            AdditionalAddressInfo = encryptManager.Dencrypt(AdditionalAddressInfo, salt);
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
            StateProvince = encryptManager.Encrypt(StateProvince, salt);
            Address = encryptManager.Encrypt(Address, salt);
            Number = encryptManager.Encrypt(Number, salt);
            AdditionalAddressInfo = encryptManager.Encrypt(AdditionalAddressInfo, salt);
            Neighborhood = encryptManager.Encrypt(Neighborhood, salt);
            NetworksName = encryptManager.Encrypt(NetworksName, salt);
            NetworksPassword = encryptManager.Encrypt(NetworksPassword, salt);

            _encrypted = true;
        }
    }
}
