namespace IntelligentHabitacion.Api.Domain.Repository.Token
{
    public interface ITokenWriteOnlyRepository
    {
        void Add(Entity.Token token);
    }
}
