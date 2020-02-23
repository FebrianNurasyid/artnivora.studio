using FluentMigrator;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200131_1)]
    public class Migration20200131_AddMessages : DatabaseMigration
    {

        public override void Up()
        {
            Create.Table("MessageAttachment")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FilePath").AsString()
                .WithColumn("ContentType").AsString();

            Create.Table("MessageContact")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid()
                .WithColumn("UserFullName").AsString();

            Create.Table("Message")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("MailboxUserId").AsGuid()
                .WithColumn("Sender").AsGuid()
                .WithColumn("Subject").AsString()
                .WithColumn("Body").AsString(int.MaxValue)
                .WithColumn("IsRead").AsBoolean()
                .WithColumn("IsArchived").AsBoolean()
                .WithColumn("MessageSendDateTime").AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("Recipients")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("MessageId").AsGuid()
                .WithColumn("MessageContactId").AsGuid();

            Create.Table("MessageAttachments")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("MessageId").AsGuid()
                .WithColumn("MessageAttachementId").AsGuid();

            Create.ForeignKey("MessageContact_User")
                .FromTable("MessageContact").ForeignColumn("UserId")
                .ToTable("User").PrimaryColumn("Id");

            Create.ForeignKey("Message_MailboxUser")
                .FromTable("Message").ForeignColumn("MailboxUserId")
                .ToTable("User").PrimaryColumn("Id");

            Create.ForeignKey("Message_Sender")
                .FromTable("Message").ForeignColumn("Sender")
                .ToTable("MessageContact").PrimaryColumn("Id");

            Create.ForeignKey("Recipients_Message")
                .FromTable("Recipients").ForeignColumn("MessageId")
                .ToTable("Message").PrimaryColumn("Id");

            Create.ForeignKey("Recipients_MessageContact")
                .FromTable("Recipients").ForeignColumn("MessageContactId")
                .ToTable("MessageContact").PrimaryColumn("Id");
        }
        public override void Down()
        {
            Delete.ForeignKey("Recipients_MessageContact");
            Delete.ForeignKey("Recipients_Message");
            Delete.ForeignKey("Message_Sender");
            Delete.ForeignKey("Message_MailboxUser");
            Delete.ForeignKey("MessageContact_User");

            Delete.Table("MessageAttachments");
            Delete.Table("Recipients");
            Delete.Table("Message");
            Delete.Table("MessageContact");
            Delete.Table("MessageAttachment");
        }

    }
}
