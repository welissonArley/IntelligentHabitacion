using IntelligentHabitacion.Api.Repository.Model;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Repository.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByEmail(string email);
        List<User> GetByHome(long homeId);
        bool EmailAlreadyBeenRegistered(string email);
        long? GetHomeId(long userId);
    }
}
