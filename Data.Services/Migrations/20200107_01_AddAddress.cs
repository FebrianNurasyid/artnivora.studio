using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200107_1)]
	public class Migration20200107_01_AddAddress : DatabaseMigration
	{
		
		public override void Up()
		{
			Create.Table("Address")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("Street").AsString()
				.WithColumn("HouseNumber").AsString()
				.WithColumn("ZipCode").AsString()
				.WithColumn("City").AsString()
				.WithColumn("Country").AsString();
		}

		public override void Down()
		{
			Delete.Table("Address");
		}
	}
}
