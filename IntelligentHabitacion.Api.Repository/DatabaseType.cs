using IntelligentHabitacion.Api.Repository.Interface;

namespace IntelligentHabitacion.Api.Repository
{
    public class DatabaseType : IDatabaseTypeRepository
    {
        private const string DatabaseSqlServer2008 = "SQLServer2008";
        private const string DatabaseSqlServer2012 = "SQLServer2012";
        private const string DatabaseMySql = "MySQL";

        private readonly string TypeOfDataBase;
        private readonly string ConnectionString;

        public DatabaseType(string typeOfDatabase, string connectionString)
        {
            TypeOfDataBase = typeOfDatabase;
            ConnectionString = connectionString;
        }

        public string GetConectionString()
        {
            return ConnectionString;
        }

        public bool DatabaseIsSQLServer2008()
        {
            return GetDatabaseType().Equals(DatabaseSqlServer2008);
        }

        public bool DatabaseIsSQLServer2012()
        {
            return GetDatabaseType().Equals(DatabaseSqlServer2012);
        }

        public bool DatabaseIsMySQL()
        {
            return GetDatabaseType().Equals(DatabaseMySql);
        }

        private string GetDatabaseType()
        {
            return TypeOfDataBase;
        }
    }
}
