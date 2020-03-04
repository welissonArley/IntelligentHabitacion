namespace IntelligentHabitacion.Api.Repository.Interface
{
    public interface IDatabaseTypeRepository
    {
        string GetConectionString();
        bool DatabaseIsSQLServer2008();
        bool DatabaseIsSQLServer2012();
        bool DatabaseIsMySQL();
    }
}
