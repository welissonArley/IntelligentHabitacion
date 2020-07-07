using IntelligentHabitacion.Api.Repository.DatabaseInformations;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public class CodeRepository : BaseRepository<Code>, ICodeRepository
    {
        public CodeRepository(IDatabaseInformations databaseInformations): base(databaseInformations)
        {
        }

        public Code GetByCode(string code)
        {
            var tempCode = new Code
            {
                Value = code
            };
            tempCode.Encrypt();

            var result = ModelSet.FirstOrDefault(c => c.Active && c.Value.Equals(tempCode.Value));
            result?.Decrypt();

            return result;
        }

        public List<Code> GetByUser(long id)
        {
            var listCode = ModelSet.Where(c => c.Active && c.UserId == id);
            foreach (var code in listCode)
                code.Decrypt();

            return listCode.ToList();
        }

        public Code GetByUserChangeAdministrator(long id)
        {
            var code = ModelSet.FirstOrDefault(c => c.Active && c.UserId == id && c.Type == CodeType.ChangeAdministrator);
            code?.Decrypt();
            return code;
        }

        public Code GetByUserDeleteHome(long id)
        {
            var code = ModelSet.FirstOrDefault(c => c.Active && c.UserId == id && c.Type == CodeType.DeleteHome);
            code?.Decrypt();
            return code;
        }

        public Code GetByUserRemoveFriend(long id)
        {
            var code = ModelSet.FirstOrDefault(c => c.Active && c.UserId == id && c.Type == CodeType.RemoveFriend);
            code?.Decrypt();
            return code;
        }

        public Code GetByUserResetPassword(long id)
        {
            var code = ModelSet.FirstOrDefault(c => c.Active && c.UserId == id && c.Type == CodeType.ResetPassword);
            code?.Decrypt();
            return code;
        }
    }
}
