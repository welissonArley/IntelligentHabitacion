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
            var user = _dataBase.Table<UserSqlite>().FirstOrDefault();
            if (user == null)
                return null;

            user.Token = new Cryptography().Dencrypt(user.Token);
            return user;
        }

        public void Save(UserSqlite user)
        {
            Delete();

            var encryptManager = new Cryptography();
            user.Token = encryptManager.Encrypt(user.Token);

            _dataBase.Insert(user);
        }

        public void UpdateName(string newName)
        {
            UserSqlite user = Get();
            user.Name = newName;
            _dataBase.Update(user);
        }

        public void UpdateToken(string newToken)
        {
            UserSqlite user = Get();
            user.Token = new Cryptography().Encrypt(newToken);
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
