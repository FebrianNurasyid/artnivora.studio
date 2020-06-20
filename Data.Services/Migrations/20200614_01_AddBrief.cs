using FluentMigrator;
using System;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{    
	[Migration(20200601_01)]
	public class Migration_20200614_01_AddBrief : DatabaseMigration
	{
		public override void Up()
		{
			Create.Table("Brief")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("DateOfBrief").AsDateTime2().NotNullable()
				.WithColumn("Descriptions").AsString()
				.WithColumn("Industry").AsString()
				.WithColumn("Classic").AsString()
				.WithColumn("Mature").AsString()
				.WithColumn("Feminine").AsString()
				.WithColumn("Playful").AsString()
				.WithColumn("Economical").AsString()
				.WithColumn("Geometric").AsString()
				.WithColumn("Abstract").AsString()
				.WithColumn("DesignInspiration").AsString()
				.WithColumn("OtherNotes").AsString()
				.WithColumn("CreatedDate").AsDateTime2().Nullable()
				.WithColumn("CreatedBy").AsString()				
				.WithColumn("ModifiedDate").AsDateTime2().Nullable()
				.WithColumn("ModifiedBy").AsString().Nullable();				
		}
		public override void Down()
		{
			Delete.Table("Brief");
		}
	}
}
