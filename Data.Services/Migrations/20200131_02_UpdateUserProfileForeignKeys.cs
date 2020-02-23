using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200131_5)]
	public class Migration20200131_5_UpdateParticipantProfileAndVolunteerProfile : DatabaseMigration
	{
		
		public override void Up()
		{
            Delete.ForeignKey("ParticipantProfile_UserProfile").OnTable("ParticipantProfile");
            Delete.ForeignKey("VolunteerProfile_UserProfile").OnTable("VolunteerProfile");

            Alter.Table("VolunteerProfile").AddColumn("UserProfileId").AsGuid().Nullable();
            Alter.Table("ParticipantProfile").AddColumn("UserProfileId").AsGuid().Nullable();

            Create.ForeignKey("VolunteerProfile_UserProfile")
				.FromTable("VolunteerProfile").ForeignColumn("UserProfileId")
				.ToTable("UserProfile").PrimaryColumn("Id");

			Create.ForeignKey("ParticipantProfile_UserProfile")
				.FromTable("ParticipantProfile").ForeignColumn("UserProfileId")
				.ToTable("UserProfile").PrimaryColumn("Id");

            Alter.Table("UserProfile")
                .AddColumn("TelephoneListAgree")
                .AsBoolean().WithDefaultValue(false);

            Alter.Table("UserProfile")
                .AddColumn("HolidayWeekAgree")
                .AsBoolean().WithDefaultValue(false);
        }

		public override void Down()
		{
			Delete.ForeignKey("ParticipantProfile_UserProfile");
			Delete.ForeignKey("VolunteerProfile_UserProfile");
            Delete.Column("TelephoneListAgree").FromTable("UserProfile");
            Delete.Column("HolidayWeekAgree").FromTable("UserProfile");
        }
	}
}
