namespace IntelligentHabitacion.Api.Domain.Repository.User
{
    public interface IUserReadOnlyRepository
    {
        bool ExistActiveUserWithEmail(string email);
        Entity.User GetByEmail(string email);
        Entity.User GetByEmailPassword(string email, string password);
    }
}
