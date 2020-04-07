using FluentMigrator;

namespace IntelligentHabitacion.Api.Repository.DatabaseVersions
{
    [Migration((long)EnumVersions.RegisterHome, "Create table to save the homes's informations")]
    public class Version0000002 : Migration
    {
        public override void Down() {}

        public override void Up()
        {
            BaseVersion.CreateDefaultColumns(Create.Table("Home"))
                .WithColumn("ZipCode").AsString().NotNullable()
                .WithColumn("City").AsString().NotNullable()
                .WithColumn("State").AsString().NotNullable()
                .WithColumn("Country").AsString().NotNullable()
                .WithColumn("CountryAbbreviation").AsString().NotNullable()
                .WithColumn("Address").AsString().NotNullable()
                .WithColumn("Number").AsString().NotNullable()
                .WithColumn("Complement").AsString().Nullable()
                .WithColumn("Neighborhood").AsString().NotNullable()
                .WithColumn("NetworksName").AsString().Nullable()
                .WithColumn("NetworksPassword").AsString().Nullable()
                .WithColumn("AdministratorId").AsInt64().NotNullable().ForeignKey("FK_Home_User_Id", "User", "Id");

            Alter.Table("User")
                .AddColumn("HomeId").AsInt64().Nullable().ForeignKey("FK_User_Home_Id", "Home", "Id");
        }
    }
}
