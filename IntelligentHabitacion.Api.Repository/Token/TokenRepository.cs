using IntelligentHabitacion.Api.Repository.Cryptography;
using IntelligentHabitacion.Api.Repository.DatabaseInformations;
using IntelligentHabitacion.Exception.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Token
{
    public class TokenRepository : DbContext, ITokenRepository
    {
        private readonly IDatabaseInformations _databaseInformations;

        protected virtual DbSet<Token> ModelSet { get; set; }

        public TokenRepository(IDatabaseInformations databaseInformations)
        {
            _databaseInformations = databaseInformations;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (_databaseInformations.DatabaseType())
            {
                case DatabaseInformations.DatabaseType.MySql:
                    {
                        optionsBuilder.UseMySql(_databaseInformations.ConectionString());
                    }
                    break;
                default:
                    {
                        throw new UnknownDatabaseException();
                    }
            }
        }

        public void Create(Token token)
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey();
            token.Value = encryptManager.Encrypt(token.Value, salt);

            var tokenCreated = Get(token.UserId);
            if (tokenCreated == null)
                ModelSet.Add(token);
            else
                tokenCreated.Value = token.Value;

            SaveChanges();
        }

        public Token Get(long userId)
        {
            var token = ModelSet.FirstOrDefault(c => c.UserId == userId);
            if (token == null)
                return null;

            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey();
            token.Value = encryptManager.Dencrypt(token.Value, salt);

            return token;
        }
    }
}
