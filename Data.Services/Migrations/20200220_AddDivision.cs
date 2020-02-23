using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200220_1)]
	public class Migration_20200220_AddDivision : DatabaseMigration
	{
		public override void Up()
		{
			Create.Table("Mst_Division")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("DivisionName").AsString()
				.WithColumn("CretedDate").AsDateTime2()
				.WithColumn("CretedBy").AsString()
				.WithColumn("ModifiedDate").AsDateTime2()
				.WithColumn("ModifiedBy").AsString();
		}
		public override void Down()
		{
			Delete.Table("Mst_Division");
		}
	}
}
