using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200221_1)]
    public class Migration_20200221_AddProduction : DatabaseMigration
    {
		public override void Up()
		{
			Create.Table("Production")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("Title").AsString()
				.WithColumn("Category").AsString()
				.WithColumn("Themes").AsString()
				.WithColumn("Concept").AsString()
				.WithColumn("Status").AsString()			
				.WithColumn("CreatedDate").AsDateTime2()
				.WithColumn("CreatedBy").AsString()
				.WithColumn("ModifiedDate").AsDateTime2().Nullable()
				.WithColumn("ModifiedBy").AsString().Nullable()
				.WithColumn("UploadedDate").AsDateTime2().Nullable()
				.WithColumn("UploadedBy").AsString().Nullable()
				.WithColumn("UploadedStatus").AsString().Nullable();
		}
		public override void Down()
		{
			Delete.Table("TblProduction");
		}
	}
}
