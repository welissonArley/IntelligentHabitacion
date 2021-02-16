namespace IntelligentHabitacion.Api.Domain.Repository.User
{
    public interface IUserUpdateOnlyRepository
    {
        Entity.User GetById_Update(long id);
        void Update(Entity.User user);
    }
}
