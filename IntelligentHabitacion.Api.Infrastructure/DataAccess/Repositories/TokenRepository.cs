using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.Token;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class TokenRepository : ITokenWriteOnlyRepository, ITokenReadOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public TokenRepository(IntelligentHabitacionContext context) => _context = context;

        public void Add(Token token)
        {
            var tokenDatabase = _context.Tokens.FirstOrDefault(c => c.UserId == token.UserId);

            if(tokenDatabase == null)
                _context.Tokens.Add(token);
            else
            {
                tokenDatabase.Value = token.Value;
                _context.Update(tokenDatabase);
            }
        }

        public Token GetByUserId(long userId)
        {
            return _context.Tokens.AsNoTracking().FirstOrDefault(c => c.UserId == userId);
        }
    }
}
