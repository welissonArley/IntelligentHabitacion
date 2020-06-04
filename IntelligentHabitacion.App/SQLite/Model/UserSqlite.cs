using SQLite;

namespace IntelligentHabitacion.App.SQLite.Model
{
    public class UserSqlite
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfileColor { get; set; }
        public bool IsPartOfOneHome { get; set; }
        public bool IsAdministrator { get; set; }
        public string Token { get; set; }
        public double Width { get; set; }
        public bool HasOrder { get; set; }
    }
}
