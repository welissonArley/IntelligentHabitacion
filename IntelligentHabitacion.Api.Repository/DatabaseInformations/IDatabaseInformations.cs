namespace IntelligentHabitacion.Api.Repository.DatabaseInformations
{
    public interface IDatabaseInformations
    {
        string ConectionString();
        DatabaseType DatabaseType();
    }
}
