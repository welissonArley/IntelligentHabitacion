using IntelligentHabitacion.Api.Repository.Model;

namespace IntelligentHabitacion.Api.Repository.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByEmail(string email);
        bool EmailAlreadyBeenRegistered(string email);
    }
}
