using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.App.SQLite.Model;
using SQLite;

namespace IntelligentHabitacion.App.SQLite
{
    public class SqliteDatabase : ISqliteDatabase
    {
        private readonly SQLiteConnection _dataBase;

        public SqliteDatabase(ISqliteConnection sqliteConnection)
        {
            _dataBase = sqliteConnection.GetConnection();
            _dataBase.CreateTable<UserSqlite>();
        }

        public UserSqlite Get()
        {
            return _dataBase.Table<UserSqlite>().FirstOrDefault();
        }

        public void Save(UserSqlite user)
        {
            Delete();
            _dataBase.Insert(user);
        }

        public void Update(UserSqlite user)
        {
            _dataBase.Update(user);
        }

        public void Delete()
        {
            UserSqlite user = Get();
            if (user != null)
                _dataBase.Delete(user);
        }
    }
}
