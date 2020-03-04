using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.MySql;
using FluentMigrator.Runner.Processors.SqlServer;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Exception.Repository;
using System.Reflection;

namespace IntelligentHabitacion.Api.Repository.DataBaseVersions
{
    public class UpdateController
    {
        private readonly IDatabaseTypeRepository _databaseType;

        public UpdateController(IDatabaseTypeRepository databaseType)
        {
            _databaseType = databaseType;
        }

        public class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int? Timeout { get; set; }
            public string ProviderSwitches { get; set; }
        }

        public long DataBaseVersion()
        {
            var runner = GetMigrationRunner(_databaseType.GetConectionString());

            return runner.VersionLoader.VersionInfo.Latest();
        }

        public void UpdateRepository()
        {
            var runner = GetMigrationRunner(_databaseType.GetConectionString());

            runner.MigrateUp(true);
        }

        private IMigrationProcessorFactory FactoryToDabaseType()
        {
            if (_databaseType.DatabaseIsSQLServer2012())
                return new SqlServer2012ProcessorFactory();
            else if (_databaseType.DatabaseIsMySQL())
                return new MySql5ProcessorFactory();
            else if (_databaseType.DatabaseIsSQLServer2008())
                return new SqlServer2008ProcessorFactory();

            throw new UnknownDatabaseException();
        }
        private MigrationRunner GetMigrationRunner(string connectionString)
        {
            var factory = FactoryToDabaseType();

            var announcer = new ConsoleAnnouncer();
            var assembly = Assembly.GetExecutingAssembly();

            var migrationContext = new RunnerContext(announcer);
            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var processor = factory.Create(connectionString, announcer, options);
            return new MigrationRunner(assembly, migrationContext, processor);
        }
    }
}
