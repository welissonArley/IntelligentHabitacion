using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.Token;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class TokenRepository : ITokenWriteOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public TokenRepository(IntelligentHabitacionContext context) => _context = context;

        public void Add(Token token)
        {
            _context.Tokens.Add(token);
        }
    }
}
