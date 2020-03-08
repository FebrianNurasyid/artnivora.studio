using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200301_1)]
	public class Migration_20200301_AddRemarkProduction : DatabaseMigration
	{
		public override void Up()
		{
			Alter.Table("Production").AddColumn("Remark").AsString().Nullable();			
		}
		public override void Down()
		{
			Delete.Column("Remark").FromTable("Production");
		}
	}
}
