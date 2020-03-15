using SQLite;

namespace IntelligentHabitacion.App.SQLite.Interface
{
    public interface ISqliteConnection
    {
        SQLiteConnection GetConnection();
    }
}
