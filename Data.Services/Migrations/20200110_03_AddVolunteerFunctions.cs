using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200110_3)]
	public class Migration20200110_03_AddVolunteerFunctions : DatabaseMigration
	{
		
		public override void Up()
		{
			Create.Table("VolunteerFunction")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("Name").AsString()
				.WithColumn("IsCoreFunction").AsBoolean().WithDefaultValue(false)
				.WithColumn("Description").AsString().Nullable();

			Create.Table("VolunteerFunctions")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("TargetAudienceId").AsGuid()
				.WithColumn("VolunteerFunctionId").AsGuid()
				.WithColumn("Description").AsString().Nullable();


			Create.ForeignKey("VolunteerFunctions_TargetAudienceId")
				.FromTable("VolunteerFunctions").ForeignColumn("TargetAudienceId")
				.ToTable("TargetAudience").PrimaryColumn("Id");

			Create.ForeignKey("VolunteerFunctions_VolunteerFunctionId")
				.FromTable("VolunteerFunctions").ForeignColumn("VolunteerFunctionId")
				.ToTable("VolunteerFunction").PrimaryColumn("Id");

		}

		public override void Down()
		{
			Delete.ForeignKey("VolunteerFunctions_TargetAudienceId");
			Delete.ForeignKey("VolunteerFunctions_VolunteerFunctionId");

			Delete.Table("VolunteerFunctions");
			Delete.Table("VolunteerFunction");
		}
	}
}
