namespace IntelligentHabitacion.Api.Repository.Token
{
    public interface ITokenRepository
    {
        void Create(Token token);
        Token Get(long userId);
    }
}
