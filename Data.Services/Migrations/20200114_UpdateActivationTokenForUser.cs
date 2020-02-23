using FluentMigrator;
namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200114_1)]
    public class Migration_20200114_UpdateActivationTokenForUser : DatabaseMigration
    {
        public override void Up()
        {
            Delete.Column("Activation_Token").FromTable("User");
            Alter.Table("User")
                .AddColumn("Activation_Token")
                .AsGuid()
                .Nullable();            
        }

        public override void Down()
        {
            Delete.Column("Activation_Token").FromTable("User");
            Alter.Table("User")
                .AddColumn("Activation_Token")
                .AsString()
                .Nullable();
        }
    }
}
