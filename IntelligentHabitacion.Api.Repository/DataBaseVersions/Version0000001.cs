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
                .WithColumn("Name").AsString(2000).NotNullable()
                .WithColumn("Email").AsString(2000).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable();

            BaseVersion.CreateDefaultColumns(Create.Table("EmergencyContact"))
                .WithColumn("Name").AsString(2000).NotNullable()
                .WithColumn("DegreeOfKinship").AsString(2000).NotNullable()
                .WithColumn("User_Id").AsInt64().Nullable().ForeignKey("FK_EmergencyContact_User_Id", "User", "Id");

            BaseVersion.CreateDefaultColumns(Create.Table("Phonenumber"))
                .WithColumn("Number").AsString().NotNullable()
                .WithColumn("User_Id").AsInt64().Nullable().ForeignKey("FK_Phonenumber_User_Id", "User", "Id")
                .WithColumn("EmergencyContact_Id").AsInt64().Nullable().ForeignKey("FK_Phonenumber_EmergencyContact_Id", "EmergencyContact", "Id");

            Create.Table("Token")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsString(2000).NotNullable()
                .WithColumn("User_Id").AsInt64().NotNullable().ForeignKey("FK_Token_User_Id", "User", "Id");
        }
    }
}
