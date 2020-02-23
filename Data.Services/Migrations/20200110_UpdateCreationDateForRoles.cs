using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200110_1)]
	public class Migration20200110_UpdateCreationDateForRoles : DatabaseMigration
	{
		
		public override void Up()
		{
			Delete.Column("CreatedDate").FromTable("UserRoles");
			Alter.Table("UserRoles").AddColumn("CreationDate")
				.AsDateTime()
				.WithDefaultValue(SystemMethods.CurrentDateTime);

			Delete.Column("ModifiedDate").FromTable("UserRoles");
			Alter.Table("UserRoles").AddColumn("ModifiedDate")
				.AsDateTime();

		}

		public override void Down()
		{
			Delete.Column("CreationDate").FromTable("UserRoles");
			Alter.Table("UserRoles").AddColumn("CreatedDate")
				.AsDateTime2()
				.WithDefaultValue(SystemMethods.CurrentDateTime);

			Delete.Column("ModifiedDate").FromTable("UserRoles");
			Alter.Table("UserRoles").AddColumn("ModifiedDate")
				.AsDateTime2();
		}
	}
}
