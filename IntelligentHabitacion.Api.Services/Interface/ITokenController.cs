namespace IntelligentHabitacion.Api.Services.Interface
{
    public interface ITokenController
    {
        string CreateToken(string email);
        string User(string token);
    }
}
