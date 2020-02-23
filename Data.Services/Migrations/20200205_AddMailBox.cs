using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{

    [Migration(20200205_3)]
    public class Migration_20200205_AddMailBox : DatabaseMigration
    {

        public override void Up()
        {
            Create.Table("MailBox")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid()
                .WithColumn("MessageId").AsGuid()
                .WithColumn("IsRead").AsBoolean()
                .WithColumn("IsArchived").AsBoolean();

            Create.ForeignKey("MailBox_User")
                .FromTable("MailBox").ForeignColumn("UserId")
                .ToTable("User").PrimaryColumn("Id");

            Create.ForeignKey("MailBox_Message")
                .FromTable("MailBox").ForeignColumn("MessageId")
                .ToTable("Message").PrimaryColumn("Id");

            Delete.Column("IsRead").FromTable("Message");
            Delete.Column("IsArchived").FromTable("Message");
            Delete.ForeignKey("Message_MailboxUser").OnTable("Message");
            Delete.Column("MailboxUserId").FromTable("Message");
        }

        public override void Down()
        {
            Delete.ForeignKey("MailBox_User").OnTable("MailBox");
            Delete.ForeignKey("MailBox_Message").OnTable("MailBox");
            Delete.Table("MailBox");
        }
    }
}
