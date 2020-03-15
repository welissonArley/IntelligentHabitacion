using IntelligentHabitacion.App.SQLite.Interface;
using SQLite;

namespace IntelligentHabitacion.App.Droid.SQLite
{
    public class SqliteDatabaseAndroid : ISqliteConnection
    {
        public SQLiteConnection GetConnection()
        {
            var nomeDB = "intelligentHabitacion.db3";
            var caminho = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), nomeDB);
            return new SQLiteConnection(caminho);
        }
    }
}