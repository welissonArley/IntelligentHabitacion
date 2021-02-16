namespace IntelligentHabitacion.Api.Domain.Repository.Token
{
    public interface ITokenReadOnlyRepository
    {
        Entity.Token GetByUserId(long id);
    }
}
