using FluentMigrator;

namespace IntelligentHabitacion.Api.Repository.DatabaseVersions
{
    [Migration((long)EnumVersions.PushNotification, "Add push Notification Id")]
    public class Version0000005 : Migration
    {
        public override void Down() { }
        public override void Up()
        {
            Alter.Table("User")
                .AddColumn("PushNotificationId").AsString(2000).NotNullable();
        }
    }
}
