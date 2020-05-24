using FluentMigrator;

namespace IntelligentHabitacion.Api.Repository.DatabaseVersions
{
    [Migration((long)EnumVersions.RegisterFinances, "Create table and column to save finance information")]
    public class Version0000004 : Migration
    {
        public override void Down() { }
        public override void Up()
        {
            Alter.Table("HomeAssociation")
                .AddColumn("RentAmount").AsDecimal().NotNullable();
        }
    }
}
