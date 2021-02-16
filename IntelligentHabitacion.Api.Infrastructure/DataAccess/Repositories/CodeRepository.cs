using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using System.Linq;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class CodeRepository : ICodeWriteOnlyRepository, ICodeReadOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public CodeRepository(IntelligentHabitacionContext context) => _context = context;

        public void Add(Code code)
        {
            DeleteAll(code.UserId);

            _context.Codes.Add(code);
        }

        public void DeleteAllFromTheUser(long userId)
        {
            DeleteAll(userId);
        }

        public Code GetByUserId(long userId)
        {
            return _context.Codes.FirstOrDefault(c => c.UserId == userId);
        }

        private void DeleteAll(long userId)
        {
            var codes = _context.Codes.Where(c => c.UserId == userId);

            _context.Codes.RemoveRange(codes);
        }
    }
}
