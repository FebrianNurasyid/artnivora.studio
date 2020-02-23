using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20191206_2)]
	public class Migration20191206AddCreationDateToUser : DatabaseMigration
	{
		
		public override void Up()
		{
			Alter.Table("User")
				.AddColumn("CreationDate")
				.AsDateTime()
				.WithDefaultValue(SystemMethods.CurrentDateTime);
		}

		public override void Down()
		{
			Alter.Column("CreationDate").OnTable("User");
		}
	}
}
