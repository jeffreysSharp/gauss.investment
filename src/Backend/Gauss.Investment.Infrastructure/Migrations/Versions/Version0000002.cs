using FluentMigrator;

namespace Gauss.Investment.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_INVESTMENT, "Create table to save the investment' information")]
    public class Version0000002 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Investments")
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("RiskLevel").AsInt32().NotNullable()
                .WithColumn("LiquidityInDays").AsInt32().NotNullable()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_Investment_User_Id", "Users", "Id");
        }
    }
}
