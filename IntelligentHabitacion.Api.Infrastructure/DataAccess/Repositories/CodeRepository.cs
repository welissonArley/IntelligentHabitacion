using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class CodeRepository : ICodeWriteOnlyRepository, ICodeReadOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public CodeRepository(IntelligentHabitacionContext context) => _context = context;

        public async Task Add(Code code)
        {
            DeleteAll(code.UserId);

            await _context.Codes.AddAsync(code);
        }

        public void DeleteAllFromTheUser(long userId)
        {
            DeleteAll(userId);
        }

        public async Task<Code> GetByCode(string code)
        {
            return await _context.Codes.FirstOrDefaultAsync(c => c.Value.Equals(code));
        }

        public async Task<Code> GetByUserId(long userId)
        {
            return await _context.Codes.FirstOrDefaultAsync(c => c.UserId == userId && c.Active);
        }

        private void DeleteAll(long userId)
        {
            var codes = _context.Codes.Where(c => c.UserId == userId);

            _context.Codes.RemoveRange(codes);
        }
    }
}
