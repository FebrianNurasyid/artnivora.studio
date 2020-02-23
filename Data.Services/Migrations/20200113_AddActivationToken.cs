using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200113_1)]
    public class Migration_20200113AddActivationToken : DatabaseMigration
    {
        public override void Up()
        {
            Alter.Table("User")
                .AddColumn("Activation_Token")
                .AsString()
                .Nullable();

            Alter.Table("User")
            .AddColumn("Account_Is_Activated")
            .AsBoolean().WithDefaultValue(false);
        }

        public override void Down()
        {
            Alter.Column("Activation_Token").OnTable("User");
            Alter.Column("Account_Is_Activated").OnTable("User");
        }
    }
}
