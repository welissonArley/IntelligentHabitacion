using FluentMigrator;

namespace IntelligentHabitacion.Api.Repository.DataBaseVersions
{
    [Migration((long)EnumVersions.RegisterUser, "Create table to save the user's informations")]
    public class Version0000001 : Migration
    {
        public override void Down() {}

        public override void Up()
        {
            BaseVersion.CreateDefaultColumns(Create.Table("User"))
                .WithColumn("Name").AsString(1000).NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable();

            BaseVersion.CreateDefaultColumns(Create.Table("EmergencyContact"))
                .WithColumn("Name").AsString(1000).NotNullable()
                .WithColumn("DegreeOfKinship").AsString().NotNullable()
                .WithColumn("User_Id").AsInt64().Nullable().ForeignKey("FK_EmergencyContact_User_Id", "User", "Id");

            BaseVersion.CreateDefaultColumns(Create.Table("Phonenumber"))
                .WithColumn("Number").AsString().NotNullable()
                .WithColumn("User_Id").AsInt64().Nullable().ForeignKey("FK_Phonenumber_User_Id", "User", "Id")
                .WithColumn("EmergencyContact_Id").AsInt64().Nullable().ForeignKey("FK_Phonenumber_EmergencyContact_Id", "EmergencyContact", "Id");
        }
    }
}
