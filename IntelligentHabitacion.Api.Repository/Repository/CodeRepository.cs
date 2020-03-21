using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public class CodeRepository : BaseRepository<Code>, ICodeRepository
    {
        public List<Code> GetByUser(long id)
        {
            var listCode = ModelSet.Where(c => c.Active & c.UserId == id);
            foreach (var code in listCode)
                code.Decrypt();

            return listCode.ToList();
        }

        public Code GetByUserResetPassword(long id)
        {
            var code = ModelSet.FirstOrDefault(c => c.Active & c.UserId == id && c.Type == CodeType.ResetPassword);
            code?.Decrypt();
            return code;
        }
    }
}
