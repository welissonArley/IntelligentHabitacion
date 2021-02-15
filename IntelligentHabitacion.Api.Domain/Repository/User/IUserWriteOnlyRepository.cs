namespace IntelligentHabitacion.Api.Domain.Repository.User
{
    public interface IUserWriteOnlyRepository
    {
        void Add(Entity.User user);
    }
}
