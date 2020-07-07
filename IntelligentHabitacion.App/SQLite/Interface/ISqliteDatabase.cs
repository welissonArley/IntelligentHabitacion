using IntelligentHabitacion.App.SQLite.Model;

namespace IntelligentHabitacion.App.SQLite.Interface
{
    public interface ISqliteDatabase
    {
        UserSqlite Get();
        void Save(UserSqlite user);
        void Delete();
        void UpdateName(string newName);
        void UpdateToken(string newToken);
        void IsAdministrator();
        void IsNotAdministrator();
        void IsPartOfHome();
        void IsNotPartOfHome();
        void ReceivedOrder();
        void GotTheOrder();
    }
}
