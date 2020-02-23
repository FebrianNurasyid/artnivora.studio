using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200109_1)]
	public class Migration20200109_01_MakeUserProfileIdNullableForUser : DatabaseMigration
	{
		
		public override void Up()
		{
			Alter.Table("User")
				.AlterColumn("UserProfileId")
				.AsGuid().Nullable();
		}

		public override void Down()
		{
			Execute.Sql("update User set UserProfileId = 0 where UserProfileId is null");
			Alter.Table("User")
				.AlterColumn("UserProfileId")
				.AsGuid().NotNullable();
		}
	}
}
