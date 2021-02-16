using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using System.Linq;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class CodeRepository : ICodeWriteOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public CodeRepository(IntelligentHabitacionContext context) => _context = context;

        public void Add(Code code)
        {
            var codes = _context.Codes.Where(c => c.UserId == code.UserId);

            _context.Codes.RemoveRange(codes);

            _context.Codes.Add(code);
        }
    }
}
