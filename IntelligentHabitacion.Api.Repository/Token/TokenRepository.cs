using IntelligentHabitacion.Api.Repository.Cryptography;
using NHibernate;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Token
{
    public class TokenRepository
    {
        private readonly ISession Session;

        public TokenRepository(ISession session)
        {
            Session = session;
        }

        public void Create(Token token)
        {
            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey();
            token.Value = encryptManager.Encrypt(token.Value, salt);

            var tokenCreated = Get(token.User.Id);
            if (tokenCreated == null)
                Session.Save(token);
            else
            {
                tokenCreated.Value = token.Value;
                Session.Update(tokenCreated);
            }
        }

        public Token Get(long id)
        {
            var token = Session.Query<Token>().FirstOrDefault(c => c.User.Id == id);
            if (token == null)
                return null;

            var encryptManager = new Cryptography.Cryptography();
            var salt = KeyModel.GetKey();
            token.Value = encryptManager.Dencrypt(token.Value, salt);

            return token;
        }
    }
}
