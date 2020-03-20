using IntelligentHabitacion.Api.Repository.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Token
{
    public class TokenRepository : DbContext
    {
        protected virtual DbSet<Token> ModelSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=intelligenthabitacion;Uid=root;Pwd=@Ioasys;");
        }

        public void Create(Token token)
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey();
            token.Value = encryptManager.Encrypt(token.Value, salt);

            var tokenCreated = Get(token.UserId);
            if (tokenCreated == null)
            {
                ModelSet.Add(token);
                SaveChanges();
            }
            else
            {
                tokenCreated.Value = token.Value;
                SaveChanges();
            }
        }

        public Token Get(long id)
        {
            var token = ModelSet.FirstOrDefault(c => c.UserId == id);
            if (token == null)
                return null;

            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey();
            token.Value = encryptManager.Dencrypt(token.Value, salt);

            return token;
        }
    }
}
