using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
	[Migration(20200107_2)]
	public class Migration20200107_02_AddUserProfile : DatabaseMigration
	{
		
		public override void Up()
		{
			Create.Table("UserProfile")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("Salutation").AsString()
				.WithColumn("FirstName").AsString()
				.WithColumn("LastName").AsString()
				.WithColumn("Insertion").AsString().Nullable()
				.WithColumn("MaidenName").AsString().Nullable()
				.WithColumn("ContactAddressId").AsGuid()
				.WithColumn("PhoneNumber").AsString().Nullable()
				.WithColumn("Birthdate").AsDateTime().Nullable()
				.WithColumn("MobileNumber").AsString().Nullable()
                .WithColumn("ProfilePicture").AsString().Nullable();

			Create.ForeignKey("UserProfile_Address") // You can give the FK a name or just let Fluent Migrator default to one
				.FromTable("UserProfile").ForeignColumn("ContactAddressId")
				.ToTable("Address").PrimaryColumn("Id");

			Alter.Table("User")
				.AddColumn("UserProfileId")
				.AsGuid();

			Create.ForeignKey("User_UserProfileId")
				.FromTable("User").ForeignColumn("UserProfileId")
				.ToTable("UserProfile").PrimaryColumn("Id");
		}

		public override void Down()
		{
			Delete.Table("UserProfile");
			Delete.ForeignKey("User_UserProfileId");
			Delete.Column("UserProfileId").FromTable("User");
            Delete.Column("ProfilePicture").FromTable("UserProfile");
        }
	}
}
