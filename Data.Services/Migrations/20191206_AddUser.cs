using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20191206_1)]
	public class Migration20191206AddUser : DatabaseMigration
	{
		
		public override void Up()
		{
			Create.Table("User")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("Username").AsString()
				.WithColumn("Email").AsString()
				.WithColumn("Password").AsString();
		}

		public override void Down()
		{
			Delete.Table("User");
		}
	}
}
