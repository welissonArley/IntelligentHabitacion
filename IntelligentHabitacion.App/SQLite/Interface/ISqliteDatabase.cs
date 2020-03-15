using IntelligentHabitacion.App.SQLite.Model;

namespace IntelligentHabitacion.App.SQLite.Interface
{
    public interface ISqliteDatabase
    {
        UserSqlite Get();
        void Save(UserSqlite user);
        void Delete();
        void Update(UserSqlite user);
    }
}
