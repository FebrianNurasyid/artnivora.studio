using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200304_1)]
	public class _20200304_AddThema : DatabaseMigration
	{
		public override void Up()
		{
			Create.Table("Mst_Thema")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("ThemaName").AsString()
				.WithColumn("CretedDate").AsDateTime2()
				.WithColumn("CretedBy").AsString()
				.WithColumn("ModifiedDate").AsDateTime2().Nullable()
				.WithColumn("ModifiedBy").AsString().Nullable();
		}
		public override void Down()
		{
			Delete.Table("Mst_Thema");
		}
	}
}
