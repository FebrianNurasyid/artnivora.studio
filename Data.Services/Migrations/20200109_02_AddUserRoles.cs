using FluentMigrator;
using Artnivora.Studio.Portal.Business.Models;
using System;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200109_2)]
	public class Migration20200109_02_AddUserRoles : DatabaseMigration
	{
		
		public override void Up()
		{
			Create.Table("UserRole")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("Key").AsString();

			Create.Table("UserRoles")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("UserId").AsGuid()
				.WithColumn("UserRoleId").AsGuid()
				.WithColumn("CreatedDate").AsDateTime2().Nullable()
				.WithColumn("ModifiedDate").AsDateTime2().Nullable();

			Create.ForeignKey("UserRoles_UserRole")
				.FromTable("UserRoles").ForeignColumn("UserRoleId")
				.ToTable("UserRole").PrimaryColumn("Id");

			Create.ForeignKey("UserRoles_User")
				.FromTable("UserRoles").ForeignColumn("UserId")
				.ToTable("User").PrimaryColumn("Id");

			InsertAllRoles();
		}

		private void InsertAllRoles()
		{
			Insert.IntoTable("UserRole").Row(new { Id = Guid.NewGuid(), Key = UserRoleType.Admin.ToString() });
			Insert.IntoTable("UserRole").Row(new { Id = Guid.NewGuid(), Key = UserRoleType.CommissionMember.ToString() });
			Insert.IntoTable("UserRole").Row(new { Id = Guid.NewGuid(), Key = UserRoleType.Participant.ToString() });
			Insert.IntoTable("UserRole").Row(new { Id = Guid.NewGuid(), Key = UserRoleType.Volunteer.ToString() });
		}

		public override void Down()
		{
			Delete.Table("UserRoles");
			Delete.Table("UserRole");
			Delete.ForeignKey("UserRoles_UserRole");
			Delete.ForeignKey("UserRoles_User");
		}
	}
}
