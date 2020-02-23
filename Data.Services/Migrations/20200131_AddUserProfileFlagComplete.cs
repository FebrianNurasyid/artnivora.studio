using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200131_2)]
    public class Migration20200131_AddUserProfileFlagComplete : DatabaseMigration
    {
        public override void Up()
        {
            Alter.Table("UserProfile")
                .AddColumn("IsComplete")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false);

        }
        public override void Down()
        {
            Delete.Column("IsComplete").FromTable("UserProfile");
        }

    }
}
