using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Gauss.Investment.Infrastructure.Migrations.Versions
{
    public abstract class VersionBase : ForwardOnlyMigration
    {
        protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
        {
            return 
                Create.Table(table)
                       .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                       .WithColumn("CreatedOn").AsDateTime().NotNullable()
                       .WithColumn("Active").AsBoolean().NotNullable();
        }
    }
}
