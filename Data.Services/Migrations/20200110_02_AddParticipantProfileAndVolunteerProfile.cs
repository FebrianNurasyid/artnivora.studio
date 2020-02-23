using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200110_2)]
	public class Migration20200110_02_AddParticipantProfileAndVolunteerProfile : DatabaseMigration
	{
		
		public override void Up()
		{
			Create.Table("TargetAudience")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("Name").AsString()
				.WithColumn("Description").AsString().Nullable();

			Create.Table("TargetAudienceSubGroup")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("TargetAudienceId").AsGuid()
				.WithColumn("Name").AsString()
				.WithColumn("Description").AsString().Nullable();

			Create.Table("ParticipantProfile")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("HealthcareProviderId").AsString().Nullable();

			Create.Table("VolunteerProfile")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("IsCoreVolunteer").AsBoolean().WithDefaultValue(false)
				.WithColumn("MaritalStatus").AsString();

			Create.Table("VolunteerTargetAudience")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("VolunteerProfileId").AsGuid()
				.WithColumn("TargetAudienceId").AsGuid();

			Create.ForeignKey("TargetAudienceSubGroup_TargetAudience")
				.FromTable("TargetAudienceSubGroup").ForeignColumn("TargetAudienceId")
				.ToTable("TargetAudience").PrimaryColumn("Id");

			Create.ForeignKey("VolunteerTargetAudience_TargetAudience")
				.FromTable("VolunteerTargetAudience").ForeignColumn("TargetAudienceId")
				.ToTable("TargetAudience").PrimaryColumn("Id");

			Create.ForeignKey("VolunteerTargetAudience_VolunteerProfile")
				.FromTable("VolunteerTargetAudience").ForeignColumn("VolunteerProfileId")
				.ToTable("VolunteerProfile").PrimaryColumn("Id");

			Create.ForeignKey("VolunteerProfile_UserProfile")
				.FromTable("VolunteerProfile").ForeignColumn("Id")
				.ToTable("UserProfile").PrimaryColumn("Id");

			Create.ForeignKey("ParticipantProfile_UserProfile")
				.FromTable("ParticipantProfile").ForeignColumn("Id")
				.ToTable("UserProfile").PrimaryColumn("Id");

		}

		public override void Down()
		{
			Delete.ForeignKey("ParticipantProfile_UserProfile");
			Delete.ForeignKey("VolunteerProfile_UserProfile");
			Delete.ForeignKey("VolunteerTargetAudience_VolunteerProfile");
			Delete.ForeignKey("VolunteerTargetAudience_TargetAudience");
			Delete.ForeignKey("ParticipantProfile_LastTargetAudience");
			Delete.ForeignKey("TargetAudienceSubGroup_TargetAudience");

			Delete.Table("TargetAudienceSubGroup");
			Delete.Table("VolunteerTargetAudience");
			Delete.Table("TargetAudience");
			Delete.Table("VolunteerProfile");
			Delete.Table("ParticipantProfile");
		}
	}
}
