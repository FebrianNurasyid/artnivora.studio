using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200128_1)]
    public class Migration20200128_AddUserProfilePicture : DatabaseMigration
    {

        public override void Up()
        {
            //Alter.Table("UserProfile")
            //    .AddColumn("ProfilePicture")
            //    .AsString()
            //    .Nullable();

        }
        public override void Down()
        {
            //Delete.Column("ProfilePicture").FromTable("UserProfile");
        }

    }
}
