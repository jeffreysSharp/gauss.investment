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
                .WithColumn("InvestmentType").AsInt32().NotNullable()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_Investment_User_Id", "Users", "Id");

            CreateTable("InvestmentTypes")
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("InvestmentId").AsGuid().NotNullable().ForeignKey("FK_InvestmentType_Investment_Id", "Investments", "Id")
                .OnDelete(System.Data.Rule.Cascade);

            CreateTable("InvestmentCategories")
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("InvestmentId").AsGuid().NotNullable().ForeignKey("FK_InvestmentCategory_Investment_Id", "Investments", "Id")
                .OnDelete(System.Data.Rule.Cascade);

            CreateTable("InvestmentIssuers")
                 .WithColumn("Name").AsInt32().NotNullable()
                 .WithColumn("InvestmentId").AsGuid().NotNullable().ForeignKey("FK_InvestmentIssuer_Investment_Id", "Investments", "Id")
                 .OnDelete(System.Data.Rule.Cascade);
        }
    }
}
