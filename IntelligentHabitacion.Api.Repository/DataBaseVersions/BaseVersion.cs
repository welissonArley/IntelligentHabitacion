using FluentMigrator.Builders.Create.Table;

namespace IntelligentHabitacion.Api.Repository.DatabaseVersions
{
    public static class BaseVersion
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax CreateDefaultColumns(ICreateTableWithColumnOrSchemaOrDescriptionSyntax Table)
        {
            return Table
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CreateDate").AsDateTime().NotNullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(1);
        }
    }
}
