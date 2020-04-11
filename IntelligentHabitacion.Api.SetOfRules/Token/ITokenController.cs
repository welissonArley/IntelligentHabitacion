namespace IntelligentHabitacion.Api.SetOfRules.Token
{
    public interface ITokenController
    {
        string CreateToken(string email);
        string User(string token);
    }
}
