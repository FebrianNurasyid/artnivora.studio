using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{

	[Migration(20200229_1)]
	public class MstCategory : DatabaseMigration
	{
		public override void Up()
		{
			Create.Table("Mst_Category")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("CategoryName").AsString()
				.WithColumn("CretedDate").AsDateTime2()
				.WithColumn("CretedBy").AsString()
				.WithColumn("ModifiedDate").AsDateTime2().Nullable()
				.WithColumn("ModifiedBy").AsString().Nullable();
		}
		public override void Down()
		{
			Delete.Table("Mst_Category");
		}
	}


}
