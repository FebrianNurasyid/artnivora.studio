using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200115_1)]
    public class Migration_20200115_AddPasswordRecoveryToUser : DatabaseMigration
    {
        public override void Up()
        {
            Alter.Table("User")
                .AddColumn("PasswordRecoveryToken")
                .AsGuid()
                .Nullable();

            Alter.Table("User")
                .AddColumn("PasswordRecoveryTokenExpiryDate")
                .AsDateTime()
                .Nullable();
        }
        public override void Down()
        {
            Delete.Column("PasswordRecoveryToken").FromTable("User");
            Delete.Column("PasswordRecoveryTokenExpiryDate").FromTable("User");
        }

    }
}
