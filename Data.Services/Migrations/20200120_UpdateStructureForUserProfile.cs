using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200120_1)]
    public class Migration_20200120_UpdateStructureForUserProfile : DatabaseMigration
    {
        public override void Up()
        {
            Delete.ForeignKey("User_UserProfileId").OnTable("User");
            Delete.Column("UserProfileId").FromTable("User");
            Alter.Table("UserProfile").AddColumn("UserId").AsGuid().NotNullable();
            Create.ForeignKey("UserProfile_UserId")
                .FromTable("UserProfile").ForeignColumn("UserId")
                .ToTable("User").PrimaryColumn("Id");
        }
        public override void Down()
        {
            Delete.ForeignKey("UserProfile_UserId");
            Create.ForeignKey("User_UserProfileId")
                .FromTable("User").ForeignColumn("UserProfileId")
                .ToTable("UserProfile").PrimaryColumn("Id");
            Alter.Table("User")
             .AddColumn("UserProfileId")
             .AsGuid();
        }

    }
}
