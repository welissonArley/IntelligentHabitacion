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

        private UserSqlite GetWithoutDencryptToken()
        {
            return _dataBase.Table<UserSqlite>().FirstOrDefault();
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
            UserSqlite user = GetWithoutDencryptToken();
            user.Name = newName;
            _dataBase.Update(user);
        }

        public void UpdateToken(string newToken)
        {
            UserSqlite user = GetWithoutDencryptToken();
            user.Token = new Cryptography().Encrypt(newToken);
            _dataBase.Update(user);
        }

        public void IsAdministrator()
        {
            UserSqlite user = GetWithoutDencryptToken();
            user.IsAdministrator = true;
            user.IsPartOfOneHome = true;
            _dataBase.Update(user);
        }

        public void IsNotAdministrator()
        {
            UserSqlite user = GetWithoutDencryptToken();
            user.IsAdministrator = false;
            _dataBase.Update(user);
        }

        public void IsPartOfHome()
        {
            UserSqlite user = GetWithoutDencryptToken();
            user.IsPartOfOneHome = true;
            _dataBase.Update(user);
        }

        public void Delete()
        {
            UserSqlite user = GetWithoutDencryptToken();
            if (user != null)
                _dataBase.Delete(user);
        }

        public void ReceivedOrder()
        {
            UserSqlite user = GetWithoutDencryptToken();
            user.HasOrder = true;
            _dataBase.Update(user);
        }

        public void GotTheOrder()
        {
            UserSqlite user = GetWithoutDencryptToken();
            user.HasOrder = false;
            _dataBase.Update(user);
        }

        public void IsNotPartOfHome()
        {
            UserSqlite user = GetWithoutDencryptToken();
            user.IsPartOfOneHome = false;
            _dataBase.Update(user);
        }
    }
}
