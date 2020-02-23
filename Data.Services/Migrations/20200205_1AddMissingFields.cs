using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Data.Services.Migrations
{
    [Migration(20200205_1)]
    public class Migration20200205_1AddMissingFields : DatabaseMigration
    {
        public override void Up()
        {
            Alter.Table("UserProfile")
                .AddColumn("Initial").AsString().Nullable();
            Alter.Table("Address").AlterColumn("Country").AsString()
                .Nullable();
        }
        public override void Down()
        {
            Delete.Column("Initial").FromTable("UserProfile");
            Alter.Table("Address").AlterColumn("Country").AsString();
        }
    }

    [Migration(20200205_2)]
    public class Migration20200205_2AddMissingFields : DatabaseMigration
    {
        public override void Up()
        {
            Alter.Table("UserProfile")
                .AddColumn("Gender").AsString().NotNullable().WithDefaultValue("Vrouw");
        }
        public override void Down()
        {
            Delete.Column("Gender").FromTable("UserProfile");
        }
    }
}
