using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20191210_3)]
	public class Migration20191210AddTokenToUser : DatabaseMigration
	{
		
		public override void Up()
		{
			Alter.Table("User")
				.AddColumn("Token")
				.AsString()
				.Nullable();
		}

		public override void Down()
		{
			Alter.Column("Token").OnTable("User");
		}
	}
}
