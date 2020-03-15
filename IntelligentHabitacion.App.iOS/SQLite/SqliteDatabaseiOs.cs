using IntelligentHabitacion.App.SQLite.Interface;
using SQLite;
using System;

namespace IntelligentHabitacion.App.iOS.SQLite
{
    public class SqliteDatabaseiOs : ISqliteConnection
    {
        public SQLiteConnection GetConnection()
        {
            var nomeDB = "intelligentHabitacion.db3";
            var caminho = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library", nomeDB);
            return new SQLiteConnection(caminho);
        }
    }
}