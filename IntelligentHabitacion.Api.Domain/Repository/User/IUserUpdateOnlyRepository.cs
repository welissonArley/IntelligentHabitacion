namespace IntelligentHabitacion.Api.Domain.Repository.User
{
    public interface IUserUpdateOnlyRepository
    {
        Entity.User GetById_Update(long id);
        Entity.User GetByEmail_Update(string email);
        void Update(Entity.User user);
    }
}
