namespace IntelligentHabitacion.Api.Domain.Repository.Code
{
    public interface ICodeReadOnlyRepository
    {
        Entity.Code GetByUserId(long userId);
    }
}
