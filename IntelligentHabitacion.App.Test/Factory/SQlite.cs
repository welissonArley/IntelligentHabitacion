using IntelligentHabitacion.App.SQLite.Interface;
using Moq;

namespace IntelligentHabitacion.App.Test.Factory
{
    public class SQlite
    {
        public ISqliteDatabase GetMokSQLite()
        {
            var mock = new Mock<ISqliteDatabase>();
            mock.Setup(c => c.Delete());
            mock.Setup(c => c.Get()).Returns(new SQLite.Model.UserSqlite());
            mock.Setup(c => c.Save(new SQLite.Model.UserSqlite()));
            mock.Setup(c => c.UpdateName(It.IsAny<string>()));
            mock.Setup(c => c.UpdateToken(It.IsAny<string>()));

            return mock.Object;
        }
    }
}
