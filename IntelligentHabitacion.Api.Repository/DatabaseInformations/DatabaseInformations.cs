namespace IntelligentHabitacion.Api.Repository.DatabaseInformations
{
    public enum DatabaseType
    {
        MySql = 1
    }

    public class DatabaseInformations : IDatabaseInformations
    {
        private readonly string _connectionString;
        private readonly DatabaseType _databaseType;

        public DatabaseInformations(string connectionString, DatabaseType databaseType)
        {
            _databaseType = databaseType;
            _connectionString = connectionString;
        }

        public string ConectionString()
        {
            return _connectionString;
        }

        public DatabaseType DatabaseType()
        {
            return _databaseType;
        }
    }
}
