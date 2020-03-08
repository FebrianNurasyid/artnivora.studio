using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200303_1)]
	public class _20200303_AddRole : DatabaseMigration
	{
		public override void Up()
		{
			Create.Table("Mst_Role")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("RoleName").AsString()
				.WithColumn("CretedDate").AsDateTime2()
				.WithColumn("CretedBy").AsString()
				.WithColumn("ModifiedDate").AsDateTime2().Nullable()
				.WithColumn("ModifiedBy").AsString().Nullable();
		}
		public override void Down()
		{
			Delete.Table("Mst_Role");
		}
	}
}
