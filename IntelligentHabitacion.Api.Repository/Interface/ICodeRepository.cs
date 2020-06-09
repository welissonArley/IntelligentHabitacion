using IntelligentHabitacion.Api.Repository.Model;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Repository.Interface
{
    public interface ICodeRepository : IBaseRepository<Code>
    {
        List<Code> GetByUser(long id);
        Code GetByUserResetPassword(long id);
        Code GetByCode(string code);
        Code GetByUserChangeAdministrator(long id);
        Code GetByUserRemoveFriend(long id);
    }
}
