using IntelligentHabitacion.Api.Repository.Cryptography;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Repository.Model
{
    public enum Type
    {
        Unity = 0,
        Box = 1,
        Package = 2,
        Kilogram = 3
    }

    [Table("MyFood")]
    public class MyFood : ModelBase
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Manufacturer { get; set; }
        public Type Type { get; set; }
        public DateTime? DueDate { get; set; }
        public long UserId { get; set; }

        public override void Decrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Dencrypt(Name, salt);
            Manufacturer = encryptManager.Dencrypt(Manufacturer, salt);
        }

        public override void Encrypt()
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Encrypt(Name, salt);
            Manufacturer = encryptManager.Encrypt(Manufacturer, salt);
        }

        protected override string Salt()
        {
            return "O9AkfINBya";
        }
    }
}
